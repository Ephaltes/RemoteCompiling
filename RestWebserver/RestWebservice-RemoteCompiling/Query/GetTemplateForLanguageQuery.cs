using System.Collections.Generic;
using MediatR;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.JsonObjClasses;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetTemplateForLanguageQuery : IRequest<CustomResponse<string>>
    {
        public string Language { get; }

        public GetTemplateForLanguageQuery(string language)
        {
            Language = language;
        }
    }
}