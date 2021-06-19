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
    public class GetTemplateForLanguageHandler : IRequestHandler<GetTemplateForLanguageQuery,CustomResponse<string>>
    {
        public async Task<CustomResponse<string>> Handle(GetTemplateForLanguageQuery request, CancellationToken cancellationToken)
        {
                var result = await System.IO.File.ReadAllTextAsync($"./Templates/{request.Language.ToLower()}Template_{request.Version.ToLower()}.json", cancellationToken);               
                return CustomResponse.Success(result);
        }
    }
}