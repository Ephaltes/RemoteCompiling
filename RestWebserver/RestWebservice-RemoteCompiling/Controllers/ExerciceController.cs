using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/exercises")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class ExerciceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        
        public ExerciceController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateExercise(CreateExerciseCommand command )
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);
            command.Token = token;
            
            var result = await _mediator.Send(command);
            return result.ToResponse();
        }

        [HttpDelete("{id})")]
        public async Task<IActionResult> DeleteExercise([FromRoute] int id)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);

            var command = new DeleteExerciseCommand()
                          {
                              Id = id,
                              Token = token
                          };

            var result = await _mediator.Send(command);
            return result.ToResponse();   
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise([FromRoute] int id, UpdateExerciseCommand command)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);

            command.Id = id;
            command.Token = token;

            var result = await _mediator.Send(command);
            return result.ToResponse();   
        }

        [HttpPut("{id}/student/{student_exercice_id}")]
        public async Task<IActionResult> GradeExercise([FromRoute] int id, [FromRoute] string student_exercice_id, GradeExerciseCommand command)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);

            command.Id = id;
            command.Student_exercice_id = student_exercice_id;
            command.Token = token;

            var response = await _mediator.Send(command);
            return response.ToResponse();
        }
        [HttpPost("handin/{id}")]
        public async Task<IActionResult> HandInExercise([FromRoute] int id, HandinCommand command)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);

            command.Id = id;
            command.Token = token;
            
            var response = await _mediator.Send(command);
            return response.ToResponse();
        }
    }
}