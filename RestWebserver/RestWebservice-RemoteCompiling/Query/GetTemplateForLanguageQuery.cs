using System.Collections.Generic;
using MediatR;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using RestWebservice_RemoteCompiling.Helpers;

namespace RestWebservice_RemoteCompiling.Query
{
    public class GetTemplateForLanguageQuery : IRequest<CustomResponse<string>>
    {
        public string Language { get; }
        public string Version { get; }

        public GetTemplateForLanguageQuery(string language,string version,IAliasHelper _AliasHelper)
        {
            Language = _AliasHelper.GetAlias(language) ?? language;
            Version = version;
        }
    }
}