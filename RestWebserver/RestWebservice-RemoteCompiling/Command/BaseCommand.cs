using System.IdentityModel.Tokens.Jwt;

using MediatR;

using RestWebservice_RemoteCompiling.Entities;

namespace RestWebservice_RemoteCompiling.Command
{
    public class BaseCommand<T> : IRequest<CustomResponse<T>>
    {
        internal JwtSecurityToken Token
        {
            get;
            set;
        }
    }
}