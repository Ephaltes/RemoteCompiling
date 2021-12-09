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

        public string StdIn
        {
            get;
            set;
        }

        public List<FileEntity> Files
        {
            get;
            set;
        } = new List<FileEntity>();

        public ProjectType ProjectType
        {
            get;
            set;
        }
    }
}