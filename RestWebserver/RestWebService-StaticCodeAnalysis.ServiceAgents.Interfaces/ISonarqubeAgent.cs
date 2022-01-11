
using RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs;

using System.Threading.Tasks;

using ProjectResponse = RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs.ProjectResponse;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces
{
    public interface ISonarqubeAgent
    {
        public Task<ProjectResponse> CreateProjectAsync(string key, string name);

        public Task<IssueResponse> GetProjectIssuesAsync(ProjectResponse project);
    }
}