using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Templates")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
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
