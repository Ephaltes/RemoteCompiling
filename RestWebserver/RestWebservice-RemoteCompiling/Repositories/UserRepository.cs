using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RemoteCompileDbContext _context;

        public UserRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User newUser)
        {
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task UpdateUser(User updateUser)
        {
            _context.Users.Update(updateUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByLdapUid(string ldapUid)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.LdapUid.ToLower() == ldapUid.ToLower());
        }
    }
}