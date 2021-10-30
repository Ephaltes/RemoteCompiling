﻿using System;
using System.Collections;
using System.DirectoryServices.Protocols;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class LdapUser
    {
        public string Cn { get; init; }
        public string Mail { get; init; }
        public string Uid { get; init; }
        public string EmployeeType { get; init; }
        public string GivenName { get; init; }
        public string Ou { get; init; }
        public string Sn { get; init; }

        public LdapUser(SearchResultEntry entry)
        {
            foreach (DictionaryEntry dictionaryEntry in entry.Attributes)
            {
                DirectoryAttribute? attribute =(DirectoryAttribute) dictionaryEntry.Value;

                if (attribute?[0] is null)
                    throw new Exception($"Attribute is null for Key: {dictionaryEntry.Key}");

                switch (dictionaryEntry.Key)
                {
                     case "cn":
                         Cn = attribute[0].ToString();
                         break;
                     case "uid":
                         Uid = attribute[0].ToString();
                         break;
                     case "sn":
                         Sn = attribute[0].ToString();
                         break;
                     case "employeetype":
                         EmployeeType = attribute[0].ToString();
                         break;
                     case "mail":
                         Mail = attribute[0].ToString();
                         break;
                     case "ou":
                         Ou = attribute[0].ToString();
                         break;
                     case "givenname":
                         GivenName = attribute[0].ToString();
                         break;
                }
            }
           
        }
    }
}