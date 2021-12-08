using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Database
{
    public class ExerciseProject
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

        public virtual List<ExerciseFile> Files
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