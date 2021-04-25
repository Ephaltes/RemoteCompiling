using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Query;
using RestWebservice_RemoteCompiling.Helpers;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("Api/Templates")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    public class TemplateController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IAliasHelper _aliasHelper;
        public TemplateController(IMediator mediator,IAliasHelper aliasHelper)
        {
            _mediator = mediator;
            _aliasHelper = aliasHelper;
        }
        
        [HttpGet("{language}/{version}")]
        public async Task<IActionResult> GetTemplateForLanguage(string language,string version)
        {
            var query = new GetTemplateForLanguageQuery(language,version, _aliasHelper);
            var result = await _mediator.Send(query);
            return result.ToResponse();
        }
    }
}
