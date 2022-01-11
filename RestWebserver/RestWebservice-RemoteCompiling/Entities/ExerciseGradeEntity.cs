using System.Text.Json.Serialization;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Entities
{
    public class ExerciseGradeEntity
    {
        public int Id
        {
            get;
            set;
        }

        public UserEntity UserToGrade
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

        public ProjectEntity Project
        {
            get;
            set;
        }
    }
}