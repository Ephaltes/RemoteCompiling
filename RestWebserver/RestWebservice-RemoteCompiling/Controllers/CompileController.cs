using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Extensions;
using Serilog;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [ApiController]
    [Route("/Api/Compile")]
    [EnableCors("AllAllowedPolicy")]
    public class CompileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteCodeWithVersion([FromBody] ExecuteCodeCommand command)
        {
            Stopwatch sw = new Stopwatch();
            
            sw.Start();
            var result = await _mediator.Send(command);
            sw.Stop();
            Log.Debug($"Compile-Time Elapsed: {sw.Elapsed.TotalSeconds:0.##} s");
            
            return result.ToResponse();
        }
    }
}