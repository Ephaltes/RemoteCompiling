using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IUserRepository
    {
        public User AddUser(User newUser);

        public void UpdateUser(User updateUser);

        public User? GetUserByLdapUid(string ldapUid);
    }
}