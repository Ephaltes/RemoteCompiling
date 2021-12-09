using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWebservice_RemoteCompiling.Database
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        public virtual ICollection<Project> Projects
        {
            get;
            set;
        } = new List<Project>();
    }

    public enum UserRole
    {
        DefaultUser,
        Teacher,
        Admin
    }
}