using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using RestWebservice_RemoteCompiling.Query;
using Serilog;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GetRuntimesHandler : IRequestHandler<GetRuntimesQuery,CustomResponse<List<SupportedLanguages>>>
    {
        private readonly IPistonHelper _pistonHelper;
        
        public GetRuntimesHandler(IPistonHelper pistonHelper)
        {
            _pistonHelper = pistonHelper;
        }
        
        public async Task<CustomResponse<List<SupportedLanguages>>> Handle(GetRuntimesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result =await _pistonHelper.GetSupportedRuntimes();
                return CustomResponse.Success(result);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return CustomResponse.Error<List<SupportedLanguages>>(400,"an Error occured");
            }

        }
    }
}