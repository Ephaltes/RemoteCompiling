
using RestWebservice_StaticCodeAnalysis.DTOs;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces
{
    public interface ISonarqubeAgent
    {
        public Task<List<Services.Entities.Issue>> ScanAsync(CodeDto code);
    }
}