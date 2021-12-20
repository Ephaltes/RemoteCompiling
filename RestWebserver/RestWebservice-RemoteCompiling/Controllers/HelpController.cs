using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using RestWebservice_RemoteCompiling.Query;

using Serilog;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Help")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    public class HelpController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HelpController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("runtimes")]
        public async Task<IActionResult> GetSupportedRuntimes()
        {
            Log.Debug("Runtimes Requested");
            CustomResponse<List<SupportedLanguages>> result = await _mediator.Send(new GetRuntimesQuery());

            return result.ToResponse();
        }
        [HttpGet("License")]
        public IActionResult License()
        {
            return CustomResponse.Success("Web Api Licenced: MIT License").ToResponse();
        }
        [HttpGet("PublicGithub")]
        public IActionResult PublicGithub()
        {
            return CustomResponse.Success("https://github.com/kienboec/RemoteCompiling").ToResponse();
        }
        [HttpGet("WhatIsThisApi")]
        public IActionResult WhatIsThisApi()
        {
            //change bla bla with actual names
            return CustomResponse.Success("A student project from the University of Applied Sciences Vienna in the year 2021 (4th Semester). " +
                                          "Developers: blabla under supervision of Daniel Kienboec. This API serves as a remote compiling service.")
                .ToResponse();
        }
    }
}