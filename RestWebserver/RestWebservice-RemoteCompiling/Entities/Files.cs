using System.Collections.Generic;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class Files
    {
        public string Name
        {
            get;
            set;
        }

        public FileNodeType Type
        {
            get;
            set;
        }
        
        public CodeEntity CodeEntity
        {
            get;
            set;
        }
    }
}