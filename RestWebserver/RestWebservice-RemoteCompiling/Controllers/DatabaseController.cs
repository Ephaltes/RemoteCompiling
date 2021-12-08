using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Database")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class DatabaseController
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;

        public DatabaseController(IMediator mediator, RemoteCompileDbContext context)
        {
            _mediator = mediator;
            _userRepository = new UserRepository(context);
        }

        [HttpPost("AddFileForUser")]
        public async Task<IActionResult> AddFileForUser(AddFileForUserCommand command)
        {
            CustomResponse<bool> result = await _mediator.Send(command);

            return result.ToResponse();
        }

        [HttpPost("AddCheckpointForFile")]
        public async Task<IActionResult> AddCheckpointForFile(AddCheckpointForFileCommand command)
        {
            CustomResponse<bool> result = await _mediator.Send(command);

            return result.ToResponse();
        }


        [HttpPut("UpdateFileForUser")]
        public async Task<IActionResult> UpdateFileForUser(UpdateFileForUserCommand command)
        {
            CustomResponse<bool> result = await _mediator.Send(command);

            return result.ToResponse();
        }


        [HttpDelete("RemoveFileForUser")]
        public async Task<IActionResult> RemoveFileForUser(RemoveFileForUserCommand command)
        {
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