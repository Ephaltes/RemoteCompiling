using System.ComponentModel.DataAnnotations;
using MediatR;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.JsonObjClasses;

namespace RestWebservice_RemoteCompiling.Command
{
    public class LoginCommand : IRequest<CustomResponse<LdapUser>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}