using System;
using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Database
{
    public class Exercise
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        
        public virtual User Author
        {
            get;
            set;
        }
        
        public string Description
        {
            get;
            set;
        }
        
        public string TaskDefinition
        {
            get;
            set;
        }
        
        public DateTime DueDate
        {
            get;
            set;
        }
        
        public virtual List<ExerciseFile> Files
        {
            get;
            set;
        } = new List<ExerciseFile>();
    }
}