using RestWebService_StaticCodeAnalysis.Services.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWebService_StaticCodeAnalysis.DataAccess.Interfaces
{
    public interface IScanJobRepository
    {
        public Task<List<ScanJob>> GetAllAsync();

        public Task<ScanJob?> GetByIdAsync(int scanJobId);

        public Task<ScanJob> CreateAsync(ScanJob scanJob);

        public Task<ScanJob> UpdateAsync(ScanJob scanJob);

        public Task DeleteAsync(ScanJob scanJob);
    }
}
