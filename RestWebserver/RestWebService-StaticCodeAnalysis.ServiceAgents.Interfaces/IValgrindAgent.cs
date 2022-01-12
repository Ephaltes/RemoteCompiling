
using RestWebservice_StaticCodeAnalysis.DTOs;

using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces
{
    public interface IValgrindAgent
    {
        public Task<List<Services.Entities.Issue>> ScanCAsync(CodeDto codeDto);

        public Task<List<Services.Entities.Issue>> ScanCppAsync(CodeDto codeDto);
    }
}