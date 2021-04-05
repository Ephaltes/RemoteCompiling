using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Query;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Templates")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    public class TemplateController : ControllerBase
    {

        private readonly IMediator _mediator;
        public TemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{language}")]
        public async Task<IActionResult> GetTemplateForLanguage(string language)
        {
            var query = new GetTemplateForLanguageQuery(language);
            var result = await _mediator.Send(query);
            return result.ToResponse();
        }
    }
}
