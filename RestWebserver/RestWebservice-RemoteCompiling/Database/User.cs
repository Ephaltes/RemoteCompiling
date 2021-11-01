using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;

namespace RestWebservice_RemoteCompiling.Database
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string LdapUid { get; set; }
        public UserRole UserRole { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }

    public enum UserRole
    {
        DefaultUser,
        Teacher,
        Admin
    }
}