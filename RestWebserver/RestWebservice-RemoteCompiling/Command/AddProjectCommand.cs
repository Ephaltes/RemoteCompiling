using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class AddProjectCommand : BaseCommand<int>
    {
        public ProjectEntity Project
        {
            get;
            set;
        }
    }
}