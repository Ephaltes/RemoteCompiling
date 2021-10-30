using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Database
{
    public class Repository
    {
        private RemoteCompileDbContext _context;

        public Repository(RemoteCompileDbContext context)
        {
            _context = context;
        }
        
        public string AddUser(User newUser)
        {
            _context.Add(newUser);
            _context.SaveChanges();
            return newUser.LdapUri;
        }

        public void UpdateUser(User updateUser)
        { 
            _context.Users.Update(updateUser);
            _context.SaveChanges();
        }

        public User GetUserByLdapIdent(string userLdapUri)
        {
            return _context.Users.Include(x => x.Files).ThenInclude(x => x.Checkpoints).FirstOrDefault(x => x.LdapUri == userLdapUri);
        }
    }
}