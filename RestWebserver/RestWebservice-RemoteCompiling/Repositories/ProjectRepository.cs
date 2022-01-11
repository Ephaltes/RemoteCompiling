using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly RemoteCompileDbContext _context;

        public ProjectRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }

        public async Task<Project> Add(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project?> GetProject(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetProjectIfUserHasAccess(int id, string ldapUid)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.LdapUid.ToLower() == ldapUid.ToLower() &&
                                                                       x.Projects.Any(p => p.Id == id));

            if (user is not null)
                return await GetProject(id);

            return null;
        }

        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}