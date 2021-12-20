namespace RestWebservice_RemoteCompiling.Command
{
    public class DeleteProjectCommand : BaseCommand<bool>
    {
        public int ProjectId
        {
            get;
            set;
        }
    }
}