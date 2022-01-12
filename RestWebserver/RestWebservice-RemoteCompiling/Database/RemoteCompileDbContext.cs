using Microsoft.EntityFrameworkCore;

using Npgsql;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Database
{
    public class RemoteCompileDbContext : DbContext
    {
        public DbSet<User> Users
        {
            get;
            set;
        }

        public DbSet<File> Files
        {
            get;
            set;
        }

        public DbSet<Checkpoint> Checkpoints
        {
            get;
            set;
        }

        public DbSet<Session> Sessions
        {
            get;
            set;
        }

        public DbSet<Exercise> Exercises
        {
            get;
            set;
        }

        public DbSet<ExerciseFile?> ExerciseFiles
        {
            get;
            set;
        }

        public DbSet<ExerciseGrade> ExerciseGrades
        {
            get;
            set;
        }

        public DbSet<Project> Projects
        {
            get;
            set;
        }

        public RemoteCompileDbContext(DbContextOptions<RemoteCompileDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UserRole>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ProjectType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<GradingStatus>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<UserRole>();
            builder.HasPostgresEnum<ProjectType>();
        }
    }
}