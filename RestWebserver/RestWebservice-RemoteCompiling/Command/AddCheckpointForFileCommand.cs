using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Command
{
    public class AddCheckpointForFileCommand : BaseCommand<int>
    {
        public int FileId
        {
            get;
            set;
        }

        public Checkpoint Checkpoint
        {
            get;
            set;
        }
    }
}