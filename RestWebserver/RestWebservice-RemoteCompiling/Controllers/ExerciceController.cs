using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult DeleteExercise([FromRoute] string id)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);
            
            
            
            return CustomResponse.Success("").ToResponse();   
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExercise([FromRoute] string id, [FromBody] string body)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);
            
            
            
            return CustomResponse.Success("").ToResponse();   
        }

        [HttpPut("{id}/student/{student_exercice_id}")]
        public IActionResult Aufgabebewerten([FromRoute] string id, [FromRoute] string student_exercice_id, [FromBody] string body)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);
            
            
            
            
            return CustomResponse.Success("").ToResponse();
        }
        [HttpPost("handin/{id}")]
        public IActionResult HandInExercise([FromRoute] string id, [FromBody] string body)
        {
            string data = Request.Headers["Authorization"].ToString().Split(" ")[1];
            _tokenService.ValidateToken(data);
            var token = _tokenService.GetToken(data);
            
            
            
            
            return CustomResponse.Success(id).ToResponse();
        }
        
    }
}