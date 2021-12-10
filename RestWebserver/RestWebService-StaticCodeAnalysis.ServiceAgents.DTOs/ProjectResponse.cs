using Newtonsoft.Json;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class ProjectResponse
    {
        [JsonProperty("project")]
        public Project Details { get; set; }
    }
}
