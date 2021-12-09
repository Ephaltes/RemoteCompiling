using System;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Helpers;

namespace RestWebservice_RemoteCompiling.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public BaseController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        protected JwtSecurityToken GetTokenFromAuthorization()
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            JwtSecurityToken? token = _tokenService.GetToken(data);

            if (token is null)
                throw new AccessViolationException("Not Authenticated");

            return token;
        }
    }
}