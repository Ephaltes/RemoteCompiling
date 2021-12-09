namespace RestWebservice_RemoteCompiling.Command
{
    public class DeleteExerciseCommand : BaseCommand<bool>
    {
        public int Id
        {
            get;
            set;
        }
    }
}