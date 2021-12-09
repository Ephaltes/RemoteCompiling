using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    [Route("Api/exercises")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class ExerciceController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;

        public ExerciceController(IMediator mediator, ITokenService tokenService)
            : base(tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExercices()
        {
            CustomResponse<List<ExerciseEntity>> response = await _mediator.Send(new GetExercisesQuery());

            return response.ToResponse();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExercices(int id)
        {
            GetExerciseQuery query = new GetExerciseQuery() { Id = id };
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
            command.Token =  GetTokenFromAuthorization();

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

        [HttpPost("handin")]
        public async Task<IActionResult> HandInExercise( HandInCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<bool> response = await _mediator.Send(command);

            return response.ToResponse();
        }
    }
}