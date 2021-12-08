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
            NpgsqlConnection.GlobalTypeMapper.MapEnum<FileNodeType>();
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseFile> ExerciseFiles { get; set; }
        public DbSet<ExerciseGrade> ExerciseGrades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<UserRole>();
            builder.HasPostgresEnum<FileNodeType>();
        }
    }
}