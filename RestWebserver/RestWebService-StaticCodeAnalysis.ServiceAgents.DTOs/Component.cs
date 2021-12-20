using Newtonsoft.Json;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class Component
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("qualifier")]
        public string Qualifier { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
