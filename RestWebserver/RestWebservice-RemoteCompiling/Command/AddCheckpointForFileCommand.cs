using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Command
{
    public class AddCheckpointForFileCommand  : BaseCommand<bool>
    {
        public int FileId;
        public Checkpoint Checkpoint;
    }
}