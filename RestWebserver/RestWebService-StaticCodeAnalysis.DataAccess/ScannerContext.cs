using Microsoft.EntityFrameworkCore;

using RestWebService_StaticCodeAnalysis.Services.Entities;

namespace RestWebService_StaticCodeAnalysis.DataAccess
{
    public class ScannerContext : DbContext
    {
        public DbSet<Scan> Scans { get; set; }

        public DbSet<ScanJob> ScanJobs { get; set; }

        public ScannerContext(DbContextOptions<ScannerContext> options) : base(options)
        {
        }
    }
}
