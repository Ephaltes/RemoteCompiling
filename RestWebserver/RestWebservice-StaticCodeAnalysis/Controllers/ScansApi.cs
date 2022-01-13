using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using RestWebservice_StaticCodeAnalysis.DTOs;

using RestWebService_StaticCodeAnalysis.Services.Interfaces;
using AutoMapper;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

using RestWebservice_StaticCodeAnalysis.Interfaces;

using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;
using RestWebService_StaticCodeAnalysis.Services.Entities.Exceptions;
using Microsoft.AspNetCore.Cors;

namespace RestWebservice_StaticCodeAnalysis.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ScansApiController : ControllerBase
    {
        private readonly IScanService _scanService;

        private readonly IJwtConfiguration _jwtConfiguration;

        private readonly IMapper _mapper;

        public ScansApiController(IScanService scanService, IJwtConfiguration jwtConfiguration, IMapper mapper)
        {
            _scanService = scanService;
            _jwtConfiguration = jwtConfiguration;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a code analysis job
        /// </summary>
        /// <param name="body">Code to perform the analysis on</param>
        [HttpGet]
        [Route("/token")]
        [EnableCors("cors-allow-any")]
        [SwaggerOperation("GetAccessToken")]
        [SwaggerResponse(statusCode: 200, type: typeof(string), description: "A access token to use")]
        public virtual IActionResult GetAccessToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtConfiguration.Issuer,
                Audience = _jwtConfiguration.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string stringToken = tokenHandler.WriteToken(token);
            return Ok(stringToken);
        }

        /// <summary>
        /// Create a code analysis job
        /// </summary>
        /// <param name="body">Code to perform the analysis on</param>
        [HttpPost]
        [Route("/scans")]
        [EnableCors("cors-allow-any")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation("CreateScanJob")]
        [SwaggerResponse(statusCode: 200, type: typeof(ScanJobDto), description: "A job that corresponds to the code analysis")]
        [SwaggerResponse(statusCode: 400, description: "Not supported code-language or invalid code")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired")]
        public virtual async Task<IActionResult> CreateScanJob([FromBody] CodeDto body)
        {
            var supportedLanguages = new List<string>
            {
                "dotnet",
                "csharp",
                "mono",
                "gcc",
                "c",
                "g++",
                "c++",
                "cpp",
                "python",
                "py"
            };

            if (!supportedLanguages.Contains(body.CodeLanguage.ToLower()))
            {
                return BadRequest($"Language not supported. Only {string.Join(", ", supportedLanguages)} are supported.");
            }

            var scanJob = await _scanService.CreateScanAsync(); ;

            try
            {
                var dto = _mapper.Map<ScanJobDto>(scanJob);
                return Ok(dto);
            }
            finally
            {
                Response.OnCompleted(async () =>
                {
                    await _scanService.StartScanAsync(body, scanJob);
                });
            }
            
        }

        /// <summary>
        /// Delete a scan job and it's results
        /// </summary>
        /// <param name="scanId">Id of the scan to retrieve</param>
        [HttpDelete]
        [Route("/scans/{scanId:int}")]
        [EnableCors("cors-allow-any")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation("DeleteScanJob")]
        [SwaggerResponse(statusCode: 204, description: "Scan job was deleted")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired or user is not permitted to see this scan job")]
        [SwaggerResponse(statusCode: 404, description: "Scan with this id was not found")]
        public virtual async Task<IActionResult> DeleteScanJob([FromRoute][Required] int scanId)
        {
            try
            {
                await _scanService.DeleteScanJobAsync(scanId);
                return NoContent();
            }
            catch (ScanJobNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// See all of the current user's scan jobs
        /// </summary>
        [HttpGet]
        [Route("/scans")]
        [EnableCors("cors-allow-any")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation("GetAllScanJobs")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ScanJobDto>), description: "List of all scan jobs")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired")]
        public virtual async Task<IActionResult> GetAllScanJobs()
        {
            var jobs = await _scanService.GetAllScanJobsAsync();
            var result = _mapper.Map<List<ScanJobDto>>(jobs);
            return Ok(result);
        }

        /// <summary>
        /// Retrieve a scan job's status
        /// </summary>
        /// <param name="scanId">Id of the scan to retrieve</param>
        [HttpGet]
        [Route("/scans/{scanId:int}")]
        [EnableCors("cors-allow-any")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation("GetScanJob")]
        [SwaggerResponse(statusCode: 200, type: typeof(ScanJobDto), description: "Scan job")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired or user is not permitted to see this scan job")]
        [SwaggerResponse(statusCode: 404, description: "Scan with this scanId was not found")]
        public virtual async Task<IActionResult> GetScanJob([FromRoute][Required] int scanId)
        {
            try
            {
                var job = await _scanService.GetScanJobByIdAsync(scanId);
                var dto = _mapper.Map<ScanJobDto>(job);
                return Ok(dto);
            }
            catch (ScanJobNotFoundException)
            {
                return NotFound();
            }
            
        }

        /// <summary>
        /// Retrieves a scan's results
        /// </summary>
        /// <param name="scanId">Id of the scan to retrieve</param>
        [HttpGet]
        [Route("/scans/{scanId:int}/results")]
        [EnableCors("cors-allow-any")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation("GetScanResults")]
        [SwaggerResponse(statusCode: 200, type: typeof(ScanDto), description: "Results of the scan")]
        [SwaggerResponse(statusCode: 401, description: "Token is missing or has expired or user is not permitted to see this scan job")]
        [SwaggerResponse(statusCode: 404, description: "Scan with this scanId was not found")]
        [SwaggerResponse(statusCode: 409, description: "Scan job does not yet have status 'available'")]
        public virtual async Task<IActionResult> GetScanResults([FromRoute][Required] int scanId)
        {
            try
            {
                var job = await _scanService.GetScanJobByIdAsync(scanId);

                if (job.Status is not ScanStatus.Available)
                {
                    return Conflict();
                }

                var dto = _mapper.Map<ScanDto>(job.Scan);
                return Ok(dto);
            }
            catch (ScanJobNotFoundException)
            {
                return NotFound();
            }            
        }
    }
}
