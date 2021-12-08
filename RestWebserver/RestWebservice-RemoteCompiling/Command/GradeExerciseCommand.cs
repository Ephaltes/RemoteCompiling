namespace RestWebservice_RemoteCompiling.Command
{
    public class GradeExerciseCommand : BaseCommand<bool>
    {
        public int Id
        {
            get;
            set;
        }

        public string Student_exercice_id
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