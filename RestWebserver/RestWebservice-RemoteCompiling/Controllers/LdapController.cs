using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using Serilog;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [ApiController]
    [Route("/Api/Ldap")]
    [EnableCors("AllAllowedPolicy")]
    public class LdapController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LdapController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            CustomResponse<SessionUser> result = await _mediator.Send(command);
            return result.ToResponse();
        }
    }
}