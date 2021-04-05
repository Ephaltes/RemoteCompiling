﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWebservice_RemoteCompiling.Helpers;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Help")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    public class HelpController : ControllerBase
    {
        private readonly IPistonHelper _PisonHelper;
        public HelpController(IPistonHelper pistonHelper)
        {
            _PisonHelper = pistonHelper;
        }
        [HttpGet("runtimes")]
        public IActionResult GetSupportedRuntimes()
        {
            try
            {
                return Ok(_PisonHelper.GetSupportedRuntimes());
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("Licence")]
        public IActionResult Licence()
        {
            return Ok("\"Web Api Licence: MIT License\"");
        }
        [HttpGet("PublicGithub")]
        public IActionResult PublicGithub()
        {
            return Ok("\"https://github.com/kienboec/RemoteCompiling\"");

        }
        [HttpGet("WhatIsThisApi")]
        public IActionResult WhatIsThisApi()
        {

            //change bla bla with actual names
            return Ok("\"A student project from the University of Applied Sciences Vienna in the year 2021 (4th Semester). Developers: blabla under supervision of Daniel Kienboec. This API serves as a remote compiling service.\"");

        }
    }
}