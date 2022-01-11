using RestWebservice_StaticCodeAnalysis.DTOs;

using RestWebService_StaticCodeAnalysis.DataAccess.Interfaces;
using RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces;
using RestWebService_StaticCodeAnalysis.Services.Entities;
using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;
using RestWebService_StaticCodeAnalysis.Services.Entities.Exceptions;
using RestWebService_StaticCodeAnalysis.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using RestWebservice_StaticCodeAnalysis.Interfaces;


namespace RestWebService_StaticCodeAnalysis.Services
{
    public class ScanService : IScanService
    {
        private readonly IScanJobRepository _scanJobRepository;

        private readonly ISonarqubeAgent _scanAgent;

        private readonly ISonarqubeConfiguration _sonarqubeConfiguration;

        public ScanService(
            IScanJobRepository scanJobRepository, 
            ISonarqubeAgent scanAgent,
            ISonarqubeConfiguration configuration)
        {
            _scanJobRepository = scanJobRepository;
            _scanAgent = scanAgent;
            _sonarqubeConfiguration = configuration;
        }

        public async Task<List<ScanJob>> GetAllScanJobsAsync()
        {
            var jobs = await _scanJobRepository.GetAllAsync();
            return jobs;
        }

        public async Task<ScanJob> GetScanJobByIdAsync(int scanJobId)
        {
            var job = await _scanJobRepository.GetByIdAsync(scanJobId);

            if (job is null)
            {
                throw new ScanJobNotFoundException($"No scan job with id {scanJobId} exists.");
            }

            return job;
        }

        public async Task<ScanJob> CreateScanAsync()
        {
            var job = new ScanJob
            {
                Status = ScanStatus.Pending
            };

            job = await _scanJobRepository.CreateAsync(job);
            return job;
        }

        public async Task DeleteScanJobAsync(int scanJobId)
        {
            var job = await _scanJobRepository.GetByIdAsync(scanJobId);

            if (job is null)
            {
                throw new ScanJobNotFoundException($"No scan job with id {scanJobId} exists.");
            }

            await _scanJobRepository.DeleteAsync(job);
        }

        public async Task StartScanAsync(CodeDto codeDto, ScanJob job)
        {
            // Setup
            var intialDirectory = Directory.GetCurrentDirectory();
            var projectKey = Guid.NewGuid().ToString() + "_" + codeDto.CodeLanguage;
            var project = await _scanAgent.CreateProjectAsync(projectKey, projectKey);

            // Perform scan
            string projectPath = SetupProjectDirectoryForScan(project.Details.Key, codeDto.Code);
            Directory.SetCurrentDirectory(projectPath);

            RunEcho($"dotnet sonarscanner begin /k:\"{project.Details.Key}\" /d:sonar.host.url=\"{_sonarqubeConfiguration.ServerUrl}\"");
            var cmd1 = RunDotnet($"sonarscanner begin /k:\"{project.Details.Key}\" /d:sonar.host.url=\"{_sonarqubeConfiguration.ServerUrl}\"");

            RunEcho("dotnet build");
            var cmd2 = RunDotnet($"build");

            RunEcho("dotnet sonarscanner end");
            var cmd3 = RunDotnet($"sonarscanner end");

            // Clean up
            Directory.SetCurrentDirectory(intialDirectory);

            if (Directory.Exists(projectPath))
            {
                Directory.Delete(projectPath, true);
            }

            // If all command succeded, create scan
            if (cmd1 && cmd2 && cmd3)
            {
                // Wait a bit for sonarqube to publish it's results
                Thread.Sleep(5000);

                var response = await _scanAgent.GetProjectIssuesAsync(project);
                job.Scan = new Scan();

                var issues = response.Issues.Select(i =>
                {
                    return new Issue
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

                issues.ForEach(i => job.Scan.Issues.Add(i));
                job.Status = ScanStatus.Available;
            }
            // If a command failed, mark job as failed
            else
            {
                job.Status = ScanStatus.Failed;
            }

            await _scanJobRepository.UpdateAsync(job);
        }

        private static string SetupProjectDirectoryForScan(string projectKey, string code)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var projectListingDirectory = $"{currentDirectory}/sonar-projects";
            var projectDirectory = $"{projectListingDirectory}/{projectKey}";

            if (!Directory.Exists(projectListingDirectory))
            {
                Directory.CreateDirectory(projectListingDirectory);
            }

            if (!Directory.Exists(projectDirectory))
            {
                Directory.CreateDirectory(projectDirectory);
            }

            Directory.SetCurrentDirectory(projectDirectory);

            RunEcho("dotnet new console --force");
            RunDotnet("new console --force");

            Directory.SetCurrentDirectory(currentDirectory);

            File.WriteAllText($"{projectDirectory}/Program.cs", code);
            return projectDirectory;
        }

        private static bool RunDotnet(string arguments)
        {
            return RunProgram("dotnet", arguments);
        }

        private static bool RunEcho(string arguments)
        {
            return RunProgram("echo", arguments);
        }

        private static bool RunProgram(string fileName, string arguments)
        {
            var process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = arguments;
            process.Start();
            process.WaitForExit();
            var result = process.ExitCode;
            process.Close();
            return result == 0;
        }
    }
}
