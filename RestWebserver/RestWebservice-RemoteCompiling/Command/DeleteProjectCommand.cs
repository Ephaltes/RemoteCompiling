using MediatR;

using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Entities;

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