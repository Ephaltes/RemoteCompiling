using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using System.Web;
using System.Net.Http;
using RestWebservice_RemoteCompiling.Helpers;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Help")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly ILanguageAndVersionHelper _LanguageAndVersionHelper;
        public HelpController(ILanguageAndVersionHelper languageAndVersionHelper)
        { 
            _LanguageAndVersionHelper = languageAndVersionHelper;
        }
        [HttpGet("Languages")]
        public IActionResult GetSupportedProgrammingLanguages()
        {
            try
            {
                return Ok(_LanguageAndVersionHelper.GetSupportedLanguages());
            }
            catch (NullReferenceException e)
            {
                return NotFound();
            }
        }
        [HttpGet("{language}/SupportedVersions")]
        public IActionResult GetSupportedVersionsForLanguage(string language)
        {
            try
            {
                return Ok(_LanguageAndVersionHelper.GetSupportedVersionsForLanguage(language));
            }
            catch (NullReferenceException e)
            {
                return NotFound();
            }
        }
        [HttpGet("Licence")]
        public IActionResult Licence()
        {
            try
            {
                return Ok("\"MIT License\"");
            }
            catch (NullReferenceException e)
            {
                return NotFound();
            }
        }
        [HttpGet("PublicGithub")]
        public IActionResult PublicGithub()
        {
            try
            {
                return Ok("\"https://github.com/kienboec/RemoteCompiling\"");
            }
            catch (NullReferenceException e)
            {
                return NotFound();
            }
        }
        [HttpGet("WhatIsThisApi")]
        public IActionResult WhatIsThisApi()
        {
            try
            {
                //change bla bla with actual names
                return Ok("\"A student project from the University of Applied Sciences Vienna in the year 2021 (4th Semester). Developers: blabla under supervision of Daniel Kienboec. This API serves as a remote compiling service.\"");
            }
            catch (NullReferenceException e)
            {
                return NotFound();
            }
        }
    }
}
