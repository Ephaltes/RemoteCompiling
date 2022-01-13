
using RestWebservice_StaticCodeAnalysis.DTOs;

using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces
{
    public interface ISonarqubeAgent
    {
        public Task<List<Services.Entities.Issue>> ScanDotnetAsync(CodeDto codeDto);

        public Task<List<Services.Entities.Issue>> ScanPythonAsync(CodeDto codeDto);
    }
}