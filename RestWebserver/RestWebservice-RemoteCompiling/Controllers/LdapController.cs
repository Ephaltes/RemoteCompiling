using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;

using Serilog;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [ApiController]
    [Route("/Api/Ldap")]
    [EnableCors("AllAllowedPolicy")]
    public class LdapController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;

        public LdapController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            CustomResponse<string> result = await _mediator.Send(command);
            return result.ToResponse();
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AmILoggedIn()
        {
            string data = "";
            _tokenService.ValidateToken(data);

            return null;
        }
    }
}