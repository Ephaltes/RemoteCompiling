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

        public bool Graded
        {
            get;
            set;
        }

        public int Grading
        {
            get;
            set;
        }

        public string Feedback
        {
            get;
            set;
        }
    }
}