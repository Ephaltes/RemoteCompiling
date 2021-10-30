using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using Serilog;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand,CustomResponse<LdapUser>>
    {
        private ILdapHelper _ldapHelper;
        public LoginHandler(ILdapHelper ldapHelper)
        {
            _ldapHelper = ldapHelper;
        }
        
        public async Task<CustomResponse<LdapUser>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                LdapUser? user = _ldapHelper.LogInUser(request.Username, request.Password);

                return user is null ? CustomResponse.Error<LdapUser>(401, "Invalid Credentials") : CustomResponse.Success(user);
            }
            catch (Exception e)
            {
                Log.Error(e,$"{e.Message} \n\n{e.StackTrace}");
                return CustomResponse.Error<LdapUser>(500, "Unexpected Error");
            }
        }
        
    }
}