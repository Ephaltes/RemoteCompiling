using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Database
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

        public ICollection<ProjectEntity> Projects
        {
            get;
            set;
        } = new List<ProjectEntity>();
    }
}