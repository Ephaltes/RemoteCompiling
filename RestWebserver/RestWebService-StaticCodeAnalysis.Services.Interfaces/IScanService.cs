using RestWebservice_StaticCodeAnalysis.DTOs;

using RestWebService_StaticCodeAnalysis.Services.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWebService_StaticCodeAnalysis.Services.Interfaces
{
    public interface IScanService
    {
        public Task<List<ScanJob>> GetAllScanJobsAsync();

        public Task<ScanJob> GetScanJobByIdAsync(int scanId);

        public Task<ScanJob> CreateScanAsync();

        public Task StartScanAsync(CodeDto codeDto, ScanJob job);

        public Task DeleteScanJobAsync(int scanId);
    }
}
