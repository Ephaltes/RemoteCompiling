using RestWebService_StaticCodeAnalysis.Services.Entities;

using System.Threading.Tasks;

namespace RestWebService_StaticCodeAnalysis.DataAccess.Interfaces
{
    public interface IScanRepository
    {
        public Task<Scan> CreateAsync(Scan scan);
    }
}
