using Newtonsoft.Json;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class Rule
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("langName")]
        public string LangName { get; set; }
    }
}
