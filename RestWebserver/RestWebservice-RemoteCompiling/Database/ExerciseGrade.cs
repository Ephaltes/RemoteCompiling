using System.Text.Json.Serialization;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Database
{
    public class ExerciseGrade
    {
        public int Id
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual Exercise Exercise
        {
            get;
            set;
        }

        public virtual User UserToGrade
        {
            get;
            set;
        }

        public int? Grade
        {
            get;
            set;
        }

        public GradingStatus Status
        {
            get;
            set;
        }

        public string? Feedback
        {
            get;
            set;
        }

        public virtual ExerciseProject Project
        {
            get;
            set;
        }
    }
}