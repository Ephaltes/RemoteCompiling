using Newtonsoft.Json;

using RestWebservice_StaticCodeAnalysis.Configuration;

using RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs;
using RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces;

using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents
{
    public class SonarqubeAgent : IScanAgent
    {
        private readonly ISonarqubeConfiguration _configuration;

        public SonarqubeAgent(ISonarqubeConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ProjectResponse> CreateProjectAsync(string key, string name)
        {
            string url = $"{_configuration.ServerUrl}/api/projects/create?project={key}&name={name}";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, null);
            var content = await response.Content.ReadAsStringAsync();
            var projectDto = JsonConvert.DeserializeObject<ProjectResponse>(content);
            return projectDto;
        }

        public async Task<IssueResponse> GetProjectIssuesAsync(ProjectResponse project)
        {
            string url = $"{_configuration.ServerUrl}/api/issues/search?componentKeys={project.Details.Key}";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var issuesDto = JsonConvert.DeserializeObject<IssueResponse>(content);
            return issuesDto;
        }
    }
}