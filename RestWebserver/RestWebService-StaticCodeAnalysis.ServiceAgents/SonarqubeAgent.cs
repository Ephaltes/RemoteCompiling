using Newtonsoft.Json;

using RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs;
using RestWebService_StaticCodeAnalysis.ServiceAgents.Interfaces;

using System.Net.Http;
using System.Threading.Tasks;

using RestWebservice_StaticCodeAnalysis.Interfaces;
using System;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents
{
    public class SonarqubeAgent : ISonarqubeAgent
    {
        private readonly ISonarqubeConfiguration _configuration;

        public SonarqubeAgent(ISonarqubeConfiguration configuration)
        {
            Console.WriteLine(JsonConvert.SerializeObject(configuration));
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