using System.Collections.Generic;

using MediatR;

using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.JsonObjClasses;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetRuntimesQuery : IRequest<CustomResponse<List<SupportedLanguages>>>
    {
    }
}