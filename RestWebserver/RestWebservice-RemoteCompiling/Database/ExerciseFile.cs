using System;
using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.Database
{
    public class ExerciseFile
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

        public string FileName
        {
            get;
            set;
        }

        public virtual Checkpoint Checkpoint
        {
            get;
            set;
        }

        public FileNodeType FileNodeType
        {
            get;
            set;
        }
        
        public User User
        {
            get;
            set;
        }
        
        public Exercise Exercise
        {
            get;
            set;
        }
    }
}