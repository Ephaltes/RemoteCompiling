using System;
using System.DirectoryServices.Protocols;
using System.Net;
using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Helpers
{
    public class LdapHelper : ILdapHelper
    {
        private const string Dn = "uid={0},ou=people,dc=technikum-wien,dc=at";
        private readonly string[] _paramList = { "cn", "mail", "uid", "employeeType", "givenName", "ou", "sn" };
        /// <summary>
        /// </summary>
        /// <param name="username">ifxxx number of User</param>
        /// <param name="password">Password of User</param>
        /// <returns>LdapUser if Login Successful otherwise null</returns>
        public LdapUser? LogInUser(string username, string password)
        {
            string usernameWithDn = string.Format(Dn, username);
            NetworkCredential credentials = new NetworkCredential(usernameWithDn, password);
            LdapDirectoryIdentifier ldapServer =
                new LdapDirectoryIdentifier("ldap.technikum-wien.at", 636, true, false);

            LdapConnection connection = new LdapConnection(ldapServer, credentials, AuthType.Basic);
            connection.SessionOptions.SecureSocketLayer = true;
            connection.SessionOptions.ProtocolVersion = 3;

            DirectoryRequest request = new SearchRequest("ou=people,dc=technikum-wien,dc=at", $"uid={username}",
                SearchScope.Subtree, _paramList);

            try
            {
                connection.Bind();

                if (connection.SendRequest(request) is not SearchResponse response)
                    throw new Exception("SearchRequest yield no result");

                LdapUser user = new LdapUser(response.Entries[0]);

                return user;
            }
            catch (LdapException e)
            {
                if (e.ErrorCode == 49)
                    return null;
            }
            finally
            {
                connection.Dispose();
            }

            return null;
        }
    }
}