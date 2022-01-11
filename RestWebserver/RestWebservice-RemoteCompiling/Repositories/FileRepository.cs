using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly RemoteCompileDbContext _context;

        public FileRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UserIsOwnerOfFile(string ldapUid, int fileId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.LdapUid.ToLower() == ldapUid.ToLower() &&
                                                                     x.Projects.Any(y => y.Files.Any(z => z.Id == fileId)));

            return user is not null;
        }

        public async Task<File?> GetFile(int fileId)
        {
            return await _context.Files.FirstOrDefaultAsync(x => x.Id == fileId);
        }

        public async Task Update(File file)
        {
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
        }
    }
}