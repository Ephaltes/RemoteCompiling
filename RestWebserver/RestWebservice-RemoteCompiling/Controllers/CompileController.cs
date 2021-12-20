using System.Diagnostics;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.JsonObjClasses.Piston;

using Serilog;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [ApiController]
    [Route("/api/compile")]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class CompileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Test Comment
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ExecuteCodeWithVersion([FromBody] ExecuteCodeCommand command)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            CustomResponse<PistonCompileAndRun> result = await _mediator.Send(command);
            sw.Stop();
            Log.Debug($"Compile-Time Elapsed: {sw.Elapsed.TotalSeconds:0.##} s");

            return result.ToResponse();
        }
    }
}