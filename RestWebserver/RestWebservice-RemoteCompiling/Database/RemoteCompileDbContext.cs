using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace RestWebservice_RemoteCompiling.Database
{
    public class RemoteCompileDbContext : DbContext
    {
        public RemoteCompileDbContext(DbContextOptions<RemoteCompileDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UserRole>();
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<UserRole>();
        }
    }
}