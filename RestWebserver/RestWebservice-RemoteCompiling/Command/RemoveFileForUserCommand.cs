using MediatR;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class RemoveFileForUserCommand : BaseCommand<bool>
    {
        public int FileId { get; set; }
    }
}