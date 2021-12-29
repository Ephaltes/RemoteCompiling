using System.Linq;
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

        public User AddUser(User newUser)
        {
            _context.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public void UpdateUser(User updateUser)
        {
            _context.Users.Update(updateUser);
            _context.SaveChanges();
        }

        public async Task<User?> GetUserByLdapUid(string ldapUid)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.LdapUid.ToLower() == ldapUid.ToLower());
        }
    }
}