using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class GradeExerciseCommand : BaseCommand<bool>
    {
        public int ExerciseId
        {
            get;
            set;
        }

        public string StudentId
        {
            get;
            set;
        }

        public GradingStatus? Status
        {
            get;
            set;
        }

        public int? Grading
        {
            get;
            set;
        }

        public string? Feedback
        {
            get;
            set;
        }
    }
}