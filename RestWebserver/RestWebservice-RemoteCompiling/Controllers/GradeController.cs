using System.Net;
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
using RestWebservice_RemoteCompiling.Query;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("api/grade")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class GradeController : BaseController
    {
        private readonly IMediator _mediator;
        public GradeController(ITokenService tokenService, IMediator mediator)
            : base(tokenService)
        {
            _mediator = mediator;
        }

        [HttpGet("student/{studentId}/Exercise/{exerciseId}")]
        public async Task<IActionResult> GetGradeForStudentInExercise(string studentId, int exerciseId)
        {
            GetGradeForStudentInExerciseQuery query = new GetGradeForStudentInExerciseQuery
                                                      { ExerciseId = exerciseId, StudentId = studentId, Token = GetTokenFromAuthorization() };
            CustomResponse<ExerciseGradeEntity> response = await _mediator.Send(query);
            
            return response.ToResponse();
        }

        [HttpPut("gradeExercise")]
        public async Task<IActionResult> GradeExercise(GradeExerciseCommand command)
        {
            command.Token = GetTokenFromAuthorization();

            CustomResponse<bool> response = await _mediator.Send(command);

            return response.ToResponse();
        }
    }
}