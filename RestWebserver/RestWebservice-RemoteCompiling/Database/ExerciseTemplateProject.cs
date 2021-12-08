using System;
using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Database
{
    public class ExerciseTemplateProject
    {
        public int Id
        {
            get;
            set;
        }

        public DateTime LastModified
        {
            get;
            set;
        }

        public string ProjectName
        {
            get;
            set;
        }

        public virtual List<ExerciseTemplateFiles> Files
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