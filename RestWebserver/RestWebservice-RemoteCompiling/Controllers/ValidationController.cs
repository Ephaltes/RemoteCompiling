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
    [Route("Api/Validation")]
    [ApiController]
    public class ValidationController : ControllerBase
    {

            private readonly ILanguageAndVersionHelper _LanguageAndVersionValidator;
            private readonly HttpClient _Http;
            public ValidationController(HttpClient http, ILanguageAndVersionHelper languageAndVersionValidator)
            {
                _Http = http;
                _LanguageAndVersionValidator = languageAndVersionValidator;
            }
            [HttpPost("{language}/{version}")]
            public IActionResult ValidateCodeWithVersion(string language, string version, JSON_Code Code)
            {
                try
                {
                    if (!_LanguageAndVersionValidator.CheckConfigurationForVersionAndLanguage(language, version))
                    {
                        return NotFound();
                    }
                    /*
                    * run validation here            
                   */
                    return Ok(Code.CodeAsValue);
                }
                catch (NullReferenceException e)
                {
                    return NotFound();
                }
            }

        }
    
}
