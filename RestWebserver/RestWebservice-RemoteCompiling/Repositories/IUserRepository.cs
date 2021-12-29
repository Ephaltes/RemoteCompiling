using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IUserRepository
    {
        public User AddUser(User newUser);

        public void UpdateUser(User updateUser);

        public Task<User?> GetUserByLdapUid(string ldapUid);
    }
}