using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.Query;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("api/exercises")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class ExerciceController : BaseController
    {
        private readonly IMediator _mediator;

        public ExerciceController(IMediator mediator, ITokenService tokenService)
            : base(tokenService)
        {
            _mediator = mediator;
        }

        [HttpGet("Exercises")]
        public async Task<IActionResult> GetExercises()
        {
            CustomResponse<List<ExerciseEntity>> response = await _mediator.Send(new GetExercisesQuery());

            return response.ToResponse();
        }

        [HttpGet("Exercises/{id}")]
        public async Task<IActionResult> GetExercise(int id)
        {
            GetExerciseQuery query = new GetExerciseQuery
                                     { Id = id };
            CustomResponse<ExerciseEntity> response = await _mediator.Send(query);

            return response.ToResponse();
        }

        [HttpGet("ExercisesWithHandIn")]
        public async Task<IActionResult> GetExercisesHandIn()
        {
            CustomResponse<List<ExerciseEntity>> response = await _mediator.Send(new GetExercisesHandInQuery()
                                                                                 {
                                                                                     Token = GetTokenFromAuthorization()
                                                                                 });

            return response.ToResponse();
        }

        [HttpGet("ExercisesWithHandIn/{id}")]
        public async Task<IActionResult> GetExerciseHandIn(int id)
        {
            GetExerciseHandInQuery query = new GetExerciseHandInQuery
                                           { Id = id, Token = GetTokenFromAuthorization() };
            CustomResponse<ExerciseEntity> response = await _mediator.Send(query);

            return response.ToResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise(CreateExerciseCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<int> result = await _mediator.Send(command);

            return result.ToResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteExercise(DeleteExerciseCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<bool> result = await _mediator.Send(command);

            return result.ToResponse();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExercise(UpdateExerciseCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<bool> result = await _mediator.Send(command);

            return result.ToResponse();
        }

        [HttpPut("handin")]
        public async Task<IActionResult> HandInExercise(HandInCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<bool> response = await _mediator.Send(command);

            return response.ToResponse();
        }
    }
}