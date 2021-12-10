using Microsoft.EntityFrameworkCore;

using RestWebService_StaticCodeAnalysis.DataAccess.Interfaces;
using RestWebService_StaticCodeAnalysis.Services.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWebService_StaticCodeAnalysis.DataAccess
{
    public class ScanJobRepository : IScanJobRepository
    {
        private readonly ScannerContext _scannerContext;

        public ScanJobRepository(ScannerContext scannerContext)
        {
            _scannerContext = scannerContext;
        }

        public async Task<List<ScanJob>> GetAllAsync()
        {
            return await _scannerContext.ScanJobs.ToListAsync();
        }

        public async Task<ScanJob> GetByIdAsync(int scanJobId)
        {
            return await _scannerContext.ScanJobs.SingleOrDefaultAsync(s => s.Id == scanJobId);
        }

        public async Task<ScanJob> CreateAsync(ScanJob scanJob)
        {
            var entry = await _scannerContext.AddAsync(scanJob);
            await _scannerContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<ScanJob> UpdateAsync(ScanJob scanJob)
        {
            try
            {
                var entry = _scannerContext.Update(scanJob);
                await _scannerContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.Write(ex.InnerException.Message);
                throw;
            }
            
        }

        public async Task DeleteAsync(ScanJob scanJob)
        {
            _scannerContext.Remove(scanJob);
            await _scannerContext.SaveChangesAsync();
        }
    }
}
