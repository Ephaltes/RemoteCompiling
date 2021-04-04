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
using Microsoft.AspNetCore.Cors;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Validation")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    public class ValidationController : ControllerBase
    {

            private readonly IPistonHelper _LanguageAndVersionValidator;
            private readonly HttpClient _Http;
            public ValidationController(HttpClient http, IPistonHelper languageAndVersionValidator)
            {
                _Http = http;
                _LanguageAndVersionValidator = languageAndVersionValidator;
            }
            [HttpPost("{language}/{version}")]
            public IActionResult ValidateCodeWithVersion(string language, string version, JSON_Code Code)
            {
                try
                {
                    /*
                    * run validation here            
                   */
                    return Ok();
                }
                catch
                {
                    return NotFound();
                }
            }

        }
    
}
