using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Command
{
    public class UpdateProjectCommand : BaseCommand<bool>
    {
        public int ProjectId
        {
            get;
            set;
        }

        public string? ProjectName
        {
            get;
            set;
        }

        public string? StdIn
        {
            get;
            set;
        }

        public ProjectType? ProjectType
        {
            get;
            set;
        }
    }
}