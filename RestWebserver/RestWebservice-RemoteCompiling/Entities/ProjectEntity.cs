using System.Collections.Generic;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class ProjectEntity
    {
        public int Id
        {
            get;
            set;
        }
        public string ProjectName
        {
            get;
            set;
        }
        public string stdin
        {
            get;
            set;
        }

        public List<FileEntity> Files
        {
            get;
            set;
        } = new();
        
        public ProjectType ProjectType
        {
            get;
            set;
        }
    }
}