namespace RestWebservice_RemoteCompiling.Command
{
    public class HandInCommand : BaseCommand<bool>
    {
        public int ProjectId
        {
            get;
            set;
        }

        public int ExerciseId
        {
            get;
            set;
        }
    }
}