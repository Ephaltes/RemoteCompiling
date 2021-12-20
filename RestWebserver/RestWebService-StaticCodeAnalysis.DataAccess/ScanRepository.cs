using RestWebService_StaticCodeAnalysis.DataAccess.Interfaces;
using RestWebService_StaticCodeAnalysis.Services.Entities;

using System.Threading.Tasks;

namespace RestWebService_StaticCodeAnalysis.DataAccess
{
    public class ScanRepository : IScanRepository
    {
        private readonly ScannerContext _scannerContext;

        public ScanRepository(ScannerContext scannerContext)
        {
            _scannerContext = scannerContext;
        }

        public async Task<Scan> CreateAsync(Scan scan)
        {
            var entry = await _scannerContext.AddAsync(scan);
            await _scannerContext.SaveChangesAsync();
            return entry.Entity;
        }
    }
}
