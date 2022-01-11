using Newtonsoft.Json;

using RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs;
using RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces;

using System.Net.Http;
using System.Threading.Tasks;

using RestWebservice_StaticCodeAnalysis.Interfaces;
using System;
using System.Collections.Generic;
using RestWebservice_StaticCodeAnalysis.DTOs;
using System.IO;
using Serilog;
using RestWebService_StaticCodeAnalysis.Services.Entities.Exceptions;
using System.Threading;
using System.Linq;
using RestWebService_StaticCodeAnalysis.Services.Entities;
using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;
using System.Diagnostics;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents
{
    public class SonarqubeAgent : ISonarqubeAgent
    {
        private readonly ISonarqubeConfiguration _configuration;

        private readonly ILogger _logger;

        public SonarqubeAgent(ISonarqubeConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Services.Entities.Issue>> ScanAsync(CodeDto codeDto)
        {
            // Setup
            var projectKey = Guid.NewGuid().ToString() + "_" + codeDto.CodeLanguage;

            _logger.Information($"Creating project with key {projectKey}");
            var project = await CreateProjectAsync(projectKey, projectKey);

            PerformScan(project, codeDto);

            // Wait a bit for sonarqube to publish it's results
            Thread.Sleep(5000);

            // Get project issues from sonarqube
            var response = await GetProjectIssuesAsync(project);

            // Build issues
            var issues = response.Issues.Select(i =>
            {
                return new Services.Entities.Issue
                {
                    Component = i.Component.Replace($"{project.Details.Key}:", ""),
                    Message = i.Message,
                    Severity = Enum.Parse<IssueSeverity>(i.Severity),
                    Type = Enum.Parse<IssueType>(i.Type),
                    TextLocation = new TextLocation
                    {
                        EndLine = i.TextRange.EndLine,
                        EndOffset = i.TextRange.EndOffset,
                        StartLine = i.TextRange.StartLine,
                        StartOffset = i.TextRange.StartOffset
                    }
                };
            }).ToList();

            _logger.Information($"Found {issues.Count} issues for project");
            return issues;
        }

        private async Task<ProjectResponse> CreateProjectAsync(string key, string name)
        {
            string url = $"{_configuration.ServerUrl}/api/projects/create?project={key}&name={name}";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, null);
            var content = await response.Content.ReadAsStringAsync();
            var projectDto = JsonConvert.DeserializeObject<ProjectResponse>(content);
            return projectDto;
        }

        private async Task<IssueResponse> GetProjectIssuesAsync(ProjectResponse project)
        {
            string url = $"{_configuration.ServerUrl}/api/issues/search?componentKeys={project.Details.Key}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var issuesDto = JsonConvert.DeserializeObject<IssueResponse>(content);
            return issuesDto;
        }

        private void PerformScan(ProjectResponse project, CodeDto codeDto)
        {
            // Perform scan
            var rootDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = CreateDotnetProject(project.Details.Key, codeDto.Code);
            Directory.SetCurrentDirectory(projectDirectory);

            var beginScanCmd = RunDotnet($"sonarscanner begin /k:\"{project.Details.Key}\" /d:sonar.host.url=\"{_configuration.ServerUrl}\"");
            var buildCmd = RunDotnet($"build");
            var endScanCmd = RunDotnet($"sonarscanner end");

            // Clean up
            Directory.SetCurrentDirectory(rootDirectory);
            CleanupDotnetProject(projectDirectory);

            // Check for errors
            if (!beginScanCmd || !buildCmd || !endScanCmd)
            {
                throw new ScanFailedException("Failed to perform sonarqube scan commands");
            }
        }

        private string CreateDotnetProject(string projectName, string code)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var projectListingDirectory = $"{currentDirectory}/sonarqube-projects";
            var projectDirectory = $"{projectListingDirectory}/{projectName}";

            if (!Directory.Exists(projectListingDirectory))
            {
                Directory.CreateDirectory(projectListingDirectory);
            }

            if (!Directory.Exists(projectDirectory))
            {
                Directory.CreateDirectory(projectDirectory);
            }

            Directory.SetCurrentDirectory(projectDirectory);

            RunDotnet("new console --force");

            Directory.SetCurrentDirectory(currentDirectory);

            File.WriteAllText($"{projectDirectory}/Program.cs", code);
            return projectDirectory;
        }

        private void CleanupDotnetProject(string projectDirectory)
        {
            if (Directory.Exists(projectDirectory))
            {
                Directory.Delete(projectDirectory, true);
            }
        }

        private bool RunDotnet(params string[] arguments)
        {
            return RunProgram("dotnet", arguments);
        }

        private bool RunProgram(string fileName, params string[] arguments)
        {
            _logger.Information($"{fileName} {string.Join(' ', arguments)}");

            var process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = string.Join(' ', arguments);
            process.Start();
            process.WaitForExit();
            var result = process.ExitCode;
            process.Close();
            return result == 0;
        }
    }
}