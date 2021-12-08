using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Database")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class DatabaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public DatabaseController(IMediator mediator, RemoteCompileDbContext context, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _userRepository = new UserRepository(context);
        }

        [HttpPost("AddFileForProject/{projectId}")]
        public async Task<IActionResult> AddFileForProject([FromRoute] int projectId,AddFileForProjectCommand command)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            JwtSecurityToken? token = _tokenService.GetToken(data);
            command.Token = token;
            command.ProjectId = projectId;
            
            CustomResponse<int> result = await _mediator.Send(command);

            return result.ToResponse();
        }
        [HttpPost("AddProjectForUser")]
        public async Task<IActionResult> AddProjectForUser(AddProjectCommand command)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            JwtSecurityToken? token = _tokenService.GetToken(data);
            command.Token = token;
            
            CustomResponse<int> result = await _mediator.Send(command);

            return result.ToResponse();
        }

        [HttpPost("AddCheckpointForFile")]
        public async Task<IActionResult> AddCheckpointForFile(AddCheckpointForFileCommand command)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            JwtSecurityToken? token = _tokenService.GetToken(data);
            command.Token = token;
            
            CustomResponse<int> result = await _mediator.Send(command);

            return result.ToResponse();
        }


        [HttpPut("UpdateFileForUser")]
        public async Task<IActionResult> UpdateFileForUser(UpdateFileForProjectCommand command)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            JwtSecurityToken? token = _tokenService.GetToken(data);
            command.Token = token;
            
            CustomResponse<bool> result = await _mediator.Send(command);

            return result.ToResponse();
        }


        [HttpDelete("RemoveFileForProject")]
        public async Task<IActionResult> RemoveFileForProject(RemoveFileForProjectCommand command)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            JwtSecurityToken? token = _tokenService.GetToken(data);
            command.Token = token;
            
            CustomResponse<bool> response = await _mediator.Send(command);
            return response.ToResponse();
        }
        /*[HttpDelete("RemoveCheckpointForFile")]
        public IActionResult RemoveCheckpointForFile(string ldapIdent, int fileId, int checkpointId)
        {
            User? findUser = _userRepository.GetUserByLdapUid(ldapIdent);

            bool deleted = false;
            foreach (File file in findUser.Files)
            {
                file.LastModified = DateTime.Now;

                foreach (Checkpoint checkpoint in file.Checkpoints)
                {
                    if (checkpoint.Id != checkpointId)
                        continue;

                    file.Checkpoints.Remove(checkpoint);
                    deleted = true;

                    break;
                }
            }

            if (!deleted)
                return CustomResponse.Error<string>(401, "File not found").ToResponse();

            _userRepository.UpdateUser(findUser);

            return CustomResponse.Success("yey").ToResponse();
        } */


        [HttpGet("getUser")]
        public IActionResult GetUser(string ldapIdent)
        {
            return CustomResponse.Success(_userRepository.GetUserByLdapUid(ldapIdent)).ToResponse();
        }
    }
}