using Newtonsoft.Json;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class Reference
    {
        [JsonProperty("jira-issue-key")]
        public string JiraIssueKey { get; set; }
    }
}
