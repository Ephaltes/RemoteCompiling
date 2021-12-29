using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Database
{
    public class Project
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

        public virtual List<File> Files
        {
            get;
            set;
        } = new();
        public ProjectType ProjectType
        {
            get;
            set;
        }
        
        public int? ExerciseID
        {
            get;
            set;
        }
    }
}