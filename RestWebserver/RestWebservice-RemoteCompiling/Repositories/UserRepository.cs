using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class UserRepository : IUserRepository
    {
        private RemoteCompileDbContext _context;

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

        public User? GetUserByLdapUid(string ldapUid)
        {
            return _context.Users.FirstOrDefault(x => x.LdapUid == ldapUid.ToLower());
        }
    }
}