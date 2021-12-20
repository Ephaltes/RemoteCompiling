using Newtonsoft.Json;

using System.Collections.Generic;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class IssueResponse
    {
        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("issues")]
        public List<Issue> Issues { get; set; }

        [JsonProperty("components")]
        public List<Component> Components { get; set; }

        [JsonProperty("rules")]
        public List<Rule> Rules { get; set; }

        [JsonProperty("users")]
        public List<User> Users { get; set; }
    }
}
