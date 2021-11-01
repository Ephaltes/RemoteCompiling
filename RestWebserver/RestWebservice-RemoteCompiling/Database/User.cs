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
        public string LdapUri { get; set; }
        public UserRole UserRole { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Collection<File> Files { get; set; } = new();
    }

    public enum UserRole
    {
        DefaultUser,
        Teacher,
        Admin
    }
}