using System;
using System.Text.Json.Serialization;

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

        public ProjectType ProjectType
        {
            get;
            set;
        }

        public virtual User? User
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual ExerciseGrade? ExerciseGrade
        {
            get;
            set;
        }
    }
}