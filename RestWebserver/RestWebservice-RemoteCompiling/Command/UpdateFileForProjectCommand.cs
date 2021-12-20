namespace RestWebservice_RemoteCompiling.Command
{
    public class UpdateFileForProjectCommand : BaseCommand<bool>
    {
        public int FileId
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public int ProjectId
        {
            get;
            set;
        }
    }
}