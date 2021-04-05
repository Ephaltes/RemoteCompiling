using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using RestWebservice_RemoteCompiling.Query;
using Serilog;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class ExecuteCodeHandler : IRequestHandler<ExecuteCodeCommand,CustomResponse<PistonCompileAndRun>>
    {
        private readonly IPistonHelper _pistonHelper;
        private readonly IHttpHelper _httpHelper;
        public ExecuteCodeHandler(IPistonHelper pistonHelper, IHttpHelper httpHelper)
        {
            _pistonHelper = pistonHelper;
            _httpHelper = httpHelper;
        }
        
        public async Task<CustomResponse<PistonCompileAndRun>> Handle(ExecuteCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                SendCompileRequest sendCompileRequest = request.ToJsonSendCompileRequest(_pistonHelper);

                var response = await _httpHelper.ExecutePost("jobs", sendCompileRequest);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = JsonConvert.DeserializeObject<PistonCompileAndRun>(await response.Content.ReadAsStringAsync(cancellationToken));
                    return CustomResponse.Success(content);
                }
                else
                {
                    var content = JsonConvert.DeserializeObject<PistonError>(await response.Content.ReadAsStringAsync(cancellationToken));
                    return CustomResponse.Error<PistonCompileAndRun>(500, content?.message);
                }

            }
            catch (Exception e)
            {
                Log.Error(e,$"{e.Message} \n\n{e.StackTrace}");
                return CustomResponse.Error<PistonCompileAndRun>(500, "Unexpected Error");
            }
        }
        
    }
}