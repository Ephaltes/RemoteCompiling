using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/project")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IMediator _mediator;
        public ProjectController(ITokenService tokenService, IMediator mediator)
            : base(tokenService)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProjectForUser(AddProjectCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<int> result = await _mediator.Send(command);

            return result.ToResponse();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProject(DeleteProjectCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<bool> response = await _mediator.Send(command);

            return response.ToResponse();
        }

        [HttpDelete("update")]
        public async Task<IActionResult> UpdateProject(UpdateProjectCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<bool> response = await _mediator.Send(command);

            return response.ToResponse();
        }
    }
}