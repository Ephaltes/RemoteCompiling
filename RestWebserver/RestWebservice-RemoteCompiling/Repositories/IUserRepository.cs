using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IUserRepository
    {
        public User AddUser(User newUser);

        public void UpdateUser(User updateUser);

        public User? GetUserByLdapUid(string ldapUid);
    }
}