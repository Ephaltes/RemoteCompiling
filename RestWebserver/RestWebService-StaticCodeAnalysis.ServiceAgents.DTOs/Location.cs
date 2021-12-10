using Newtonsoft.Json;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class Location
    {
        [JsonProperty("textRange")]
        public TextRange TextRange { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}
