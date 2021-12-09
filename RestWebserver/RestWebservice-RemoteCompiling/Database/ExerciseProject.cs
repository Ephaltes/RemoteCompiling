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

        public string StdIn
        {
            get;
            set;
        }

        public virtual List<ExerciseFile> Files
        {
            get;
            set;
        } = new List<ExerciseFile>();

        public ProjectType ProjectType
        {
            get;
            set;
        }
    }
}