using RestWebservice_StaticCodeAnalysis.DTOs;

using RestWebService_StaticCodeAnalysis.DataAccess.Interfaces;
using RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces;
using RestWebService_StaticCodeAnalysis.Services.Entities;
using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;
using RestWebService_StaticCodeAnalysis.Services.Entities.Exceptions;
using RestWebService_StaticCodeAnalysis.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using System;

namespace RestWebService_StaticCodeAnalysis.Services
{
    public class ScanService : IScanService
    {
        private readonly IScanJobRepository _scanJobRepository;

        private readonly ISonarqubeAgent _sonarqubeAgent;

        private readonly IValgrindAgent _valgrindAgent;

        private readonly ILogger _logger;

        public ScanService(
            IScanJobRepository scanJobRepository, 
            ISonarqubeAgent sonarqubeAgent,
            IValgrindAgent valgrindAgent,
            ILogger logger)
        {
            _scanJobRepository = scanJobRepository;
            _sonarqubeAgent = sonarqubeAgent;
            _valgrindAgent = valgrindAgent;
            _logger = logger;
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
            try
            {
                var issues = new List<Issue>();

                switch (codeDto.CodeLanguage.ToLower())
                {
                    case "dotnet": 
                    case "csharp":
                    case "mono":
                        issues = await _sonarqubeAgent.ScanDotnetAsync(codeDto);
                        break;
                    case "gcc":
                    case "c":
                        issues = await _valgrindAgent.ScanCAsync(codeDto);
                        break;
                    case "g++":
                    case "c++":
                    case "cpp":
                        issues = await _valgrindAgent.ScanCppAsync(codeDto);
                        break;
                    case "python":
                    case "py":
                        issues = await _sonarqubeAgent.ScanPythonAsync(codeDto);
                        break;
                    default:
                        throw new LanguageNotSupportedException($"Language {codeDto.CodeLanguage} is not supported");
                }

                job.Scan = new Scan();
                issues.ForEach(i => job.Scan.Issues.Add(i));
                job.Status = ScanStatus.Available;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                job.Status = ScanStatus.Failed;
            }

            await _scanJobRepository.UpdateAsync(job);
        }

        
    }
}
