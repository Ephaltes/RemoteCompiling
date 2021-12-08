using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/exercises")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class ExerciceController
    {
        private readonly IMediator _mediator;
        private readonly IExerciceRepository _repository;
        
        public ExerciceController(IMediator mediator, IExerciceRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        [HttpPost("")]
        public IActionResult CreateExercise([FromBody] string name, [FromBody] string description)
        {

            return CustomResponse.Success("").ToResponse();
        }

        [HttpDelete("{id})")]
        public IActionResult DeleteExercise([FromRoute] string id)
        {
            return CustomResponse.Success("").ToResponse();   
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExercise([FromRoute] string id, [FromBody] string body)
        {
            return CustomResponse.Success("").ToResponse();   
        }

        [HttpPut("{id}/student/{student_exercice_id}")]
        public IActionResult Aufgabebewerten([FromRoute] string id, [FromRoute] string student_exercice_id, [FromBody] string body)
        {
            return CustomResponse.Success("").ToResponse();
        }
        [HttpPost("handin/{id}")]
        public IActionResult HandInExercise([FromRoute] string id, [FromBody] string body)
        {
            return CustomResponse.Success(id).ToResponse();
        }
        
    }
}