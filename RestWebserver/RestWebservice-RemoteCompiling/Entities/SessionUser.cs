namespace RestWebservice_RemoteCompiling.Entities
{
    public class SessionUser
    {
        public string Token { get; set; }
        public LdapUser User { get; set; }
    }
}