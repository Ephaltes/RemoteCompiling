using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class AddFileForUserCommand  : BaseCommand<bool>
    {
       public Files File { get; set; }
    }
}