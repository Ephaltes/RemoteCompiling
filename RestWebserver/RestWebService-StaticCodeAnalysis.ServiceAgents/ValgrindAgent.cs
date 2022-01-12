
using RestWebservice_StaticCodeAnalysis.DTOs;
using RestWebservice_StaticCodeAnalysis.Interfaces;

using RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces;
using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;
using RestWebService_StaticCodeAnalysis.Services.Entities.Exceptions;

using Serilog;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents
{
    public class ValgrindAgent : IValgrindAgent
    {
        private readonly IValgrindConfiguration _config;

        private readonly IValgrindReportParser _reportParser;

        private readonly ILogger _logger;

        public ValgrindAgent(IValgrindConfiguration config, IValgrindReportParser reportParser, ILogger logger)
        {
            _config = config;
            _reportParser = reportParser;
            _logger = logger;
        }

        public async Task<List<Services.Entities.Issue>> ScanCAsync(CodeDto codeDto)
        {
            var projectDirectory = CreateCProject(codeDto);
            string cFile = $"{projectDirectory}/main.c";
            string outFile = $"{projectDirectory}/main.out";
            string valgrindReportFile = $"{projectDirectory}/valgrind-out.xml";

            _logger.Information($"C file is {cFile}");
            _logger.Information($"Out file is {outFile}");
            _logger.Information($"Valgrind report file is {valgrindReportFile}");

            RunGcc($"{cFile} -g -o {outFile}");
            RunValgrind($"{_config.Flags} --xml=yes --xml-file={valgrindReportFile} {outFile}");

            // Code did not execute successfuly
            if (!File.Exists(valgrindReportFile))
            {
                throw new ScanFailedException("Failed to execute code");
            }

            var valgrindXmlContent = File.ReadAllText($"{valgrindReportFile}");
            _logger.Information(valgrindXmlContent);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(valgrindReportFile);

            var issues = _reportParser.ReadIssues(xmlDocument, "main.c");

            CleanupProject(projectDirectory);

            return issues;
        }

        public async Task<List<Services.Entities.Issue>> ScanCppAsync(CodeDto codeDto)
        {
            var projectDirectory = CreateCppProject(codeDto);
            string cFile = $"{projectDirectory}/main.cpp";
            string outFile = $"{projectDirectory}/main.out";
            string valgrindReportFile = $"{projectDirectory}/valgrind-out.xml";

            _logger.Information($"Cpp file is {cFile}");
            _logger.Information($"Out file is {outFile}");
            _logger.Information($"Valgrind report file is {valgrindReportFile}");

            RunGpp($"{cFile} -g -o {outFile}");
            RunValgrind($"{_config.Flags} --xml=yes --xml-file={valgrindReportFile} {outFile}");

            // Code did not execute successfuly
            if (!File.Exists(valgrindReportFile))
            {
                throw new ScanFailedException("Failed to execute code");
            }

            var valgrindXmlContent = File.ReadAllText($"{valgrindReportFile}");
            _logger.Information(valgrindXmlContent);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(valgrindReportFile);

            var issues = _reportParser.ReadIssues(xmlDocument, "main.cpp");

            CleanupProject(projectDirectory);

            return issues;
        }

        private static string CreateCppProject(CodeDto codeDto)
        {
            return CreateProject(codeDto, "main.cpp", "cpp-projects");
        }

        private static string CreateCProject(CodeDto codeDto)
        {
            return CreateProject(codeDto, "main.c", "c-projects");
        }

        private static string CreateProject(CodeDto codeDto, string mainFileName, string projectListingName)
        {
            var projectName = Guid.NewGuid().ToString() + "_" + codeDto.CodeLanguage;
            var currentDirectory = Directory.GetCurrentDirectory();
            var projectListingDirectory = $"{currentDirectory}/{projectListingName}";
            var projectDirectory = $"{projectListingDirectory}/{projectName}";

            if (!Directory.Exists(projectListingDirectory))
            {
                Directory.CreateDirectory(projectListingDirectory);
            }

            if (!Directory.Exists(projectDirectory))
            {
                Directory.CreateDirectory(projectDirectory);
            }

            File.WriteAllText($"{projectDirectory}/{mainFileName}", codeDto.Code);
            return projectDirectory;
        }

        private static void CleanupProject(string projectDirectory)
        {
            if (Directory.Exists(projectDirectory))
            {
                Directory.Delete(projectDirectory, true);
            }
        }

        private bool RunGcc(params string[] arguments)
        {
            return RunProgram("gcc", arguments);
        }

        private bool RunGpp(params string[] arguments)
        {
            return RunProgram("g++", arguments);
        }

        private bool RunValgrind(params string[] arguments)
        {
            return RunProgram("valgrind", arguments);
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
