using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Helpers
{
    public interface ILdapHelper
    {
        public LdapUser? LogInUser(string username, string password);
    }
}