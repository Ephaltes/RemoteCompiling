using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IUserRepository
    {
        public Task<User> AddUser(User newUser);

        public Task UpdateUser(User updateUser);

        public Task<User?> GetUserByLdapUid(string ldapUid);
    }
}