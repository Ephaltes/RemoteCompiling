using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Command
{
    public class UpdateFileForUserCommand  : BaseCommand<bool>
    {
        public int FileId;
        public string FileName;
    }
}