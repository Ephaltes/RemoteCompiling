using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IProjectRepository
    {
        public Task<Project> Add(Project project);
        public Task<Project?> GetProject(int id);
        public Task<Project?> GetProjectIfUserHasAccess(int id, string ldapUid);
        public Task Update(Project project);
    }
}