using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class HandinCommand : BaseCommand<bool>
    {
        public Files[] Files
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }
    }
}