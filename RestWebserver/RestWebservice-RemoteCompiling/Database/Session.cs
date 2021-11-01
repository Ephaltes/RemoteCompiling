using System;

namespace RestWebservice_RemoteCompiling.Database
{
    public class Session
    {
        public Guid Id
        {
            get; set;
        }

        public DateTime Expiration { get; set; } = DateTime.Now.AddHours(1);
        
        public virtual User LdapUser { get; set; }
    }
}