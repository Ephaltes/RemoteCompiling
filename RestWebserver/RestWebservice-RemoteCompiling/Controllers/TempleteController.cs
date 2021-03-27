using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Templates")]
    [ApiController]
    public class TempleteController : ControllerBase
    {
        [HttpGet("{language}")]
        public IActionResult GetTemplateForLanguage(string language)
        {
            if (!System.IO.File.Exists($"./Templates/{language.ToLower()}Template.json"))
            {
                return NotFound();
            }
            string jsonString = System.IO.File.ReadAllText($"./Templates/{language.ToLower()}Template.json");
            return Ok(jsonString);
        }
    }
}
