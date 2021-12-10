using MediatR;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class LoginCommand : IRequest<CustomResponse<string>>
    {
        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
    }
}