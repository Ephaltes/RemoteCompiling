using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using System.Net.Http;

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
