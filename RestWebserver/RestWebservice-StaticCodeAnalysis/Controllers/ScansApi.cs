using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using RestWebservice_StaticCodeAnalysis.Security;
using RestWebservice_StaticCodeAnalysis.DTOs;
using Newtonsoft.Json;

namespace RestWebservice_StaticCodeAnalysis.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ScansApiController : ControllerBase
    {
        /// <summary>
        /// Create a code analysis job
        /// </summary>
        /// <param name="body">Code to perform the analysis on</param>
        [HttpPost]
        [Route("/scans")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [SwaggerOperation("CreateScanJob")]
        [SwaggerResponse(statusCode: 200, type: typeof(ScanJobDto), description: "A job that corresponds to the code analysis")]
        [SwaggerResponse(statusCode: 400, description: "Not supported code-language or invalid code")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired")]
        public virtual IActionResult CreateScanJob([FromBody] CodeDto body)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(ScanJob));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);

            //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(401);
            string exampleJson = null;
            exampleJson = "{\n  \"id\" : 1,\n  \"status\" : \"pending\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ScanJobDto>(exampleJson)
            : default(ScanJobDto);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Delete a scan job and it's results
        /// </summary>
        /// <param name="scanId">Id of the scan to retrieve</param>
        [HttpDelete]
        [Route("/scans/{scanId}")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [SwaggerOperation("DeleteScanJob")]
        [SwaggerResponse(statusCode: 204, description: "Scan job was deleted")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired or user is not permitted to see this scan job")]
        [SwaggerResponse(statusCode: 404, description: "Scan with this id was not found")]
        public virtual IActionResult DeleteScanJob([FromRoute][Required] string scanId)
        {
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(401);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// See all of the current user's scan jobs
        /// </summary>
        [HttpGet]
        [Route("/scans")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [SwaggerOperation("GetAllScanJobs")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ScanJobDto>), description: "List of all scan jobs")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired")]
        public virtual IActionResult GetAllScanJobs()
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<ScanJob>));

            //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(401);
            string exampleJson = null;
            exampleJson = "[ {\n  \"id\" : 1,\n  \"status\" : \"pending\"\n}, {\n  \"id\" : 1,\n  \"status\" : \"pending\"\n} ]";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<ScanJobDto>>(exampleJson)
            : default(List<ScanJobDto>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Retrieve a scan job's status
        /// </summary>
        /// <param name="scanId">Id of the scan to retrieve</param>
        [HttpGet]
        [Route("/scans/{scanId}")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [SwaggerOperation("GetScanJob")]
        [SwaggerResponse(statusCode: 200, type: typeof(CodeDto), description: "Results of the scan")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired or user is not permitted to see this scan job")]
        [SwaggerResponse(statusCode: 404, description: "Scan with this scanId was not found")]
        public virtual IActionResult GetScanJob([FromRoute][Required] string scanId)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Code));

            //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(401);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\n  \"code-language\" : \"c#\",\n  \"code\" : \"code\"\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<CodeDto>(exampleJson)
            : default(CodeDto);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Retrieves a scan's results
        /// </summary>
        /// <param name="scanId">Id of the scan to retrieve</param>
        [HttpGet]
        [Route("/scans/{scanId}/results")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [SwaggerOperation("GetScanResults")]
        [SwaggerResponse(statusCode: 200, type: typeof(ScanDto), description: "Results of the scan")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired or user is not permitted to see this scan job")]
        [SwaggerResponse(statusCode: 404, description: "Scan with this scanId was not found")]
        [SwaggerResponse(statusCode: 409, description: "Scan job does not yet have status 'available'")]
        public virtual IActionResult GetScanResults([FromRoute][Required] string scanId)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Scan));

            //TODO: Uncomment the next line to return response 401 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(401);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            //TODO: Uncomment the next line to return response 409 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(409);
            string exampleJson = null;
            exampleJson = "{\n  \"total\" : 0,\n  \"issues\" : [ {\n    \"severity\" : \"minor\",\n    \"textLocation\" : {\n      \"endLine\" : 1,\n      \"endOffset\" : 1,\n      \"startOffset\" : 6,\n      \"startLine\" : 1\n    },\n    \"component\" : \"sample-project:Program.cs\",\n    \"type\" : \"CodeSmell\"\n  }, {\n    \"severity\" : \"minor\",\n    \"textLocation\" : {\n      \"endLine\" : 1,\n      \"endOffset\" : 1,\n      \"startOffset\" : 6,\n      \"startLine\" : 1\n    },\n    \"component\" : \"sample-project:Program.cs\",\n    \"type\" : \"CodeSmell\"\n  } ]\n}";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ScanDto>(exampleJson)
            : default(ScanDto);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
