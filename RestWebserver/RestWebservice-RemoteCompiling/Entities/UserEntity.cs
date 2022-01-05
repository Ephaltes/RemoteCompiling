using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class UserEntity
    {
        public string LdapUid
        {
            get;
            set;
        }

        public UserRole UserRole
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}