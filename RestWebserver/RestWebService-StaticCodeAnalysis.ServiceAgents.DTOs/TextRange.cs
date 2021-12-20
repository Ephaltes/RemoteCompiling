using Newtonsoft.Json;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class TextRange
    {
        [JsonProperty("startLine")]
        public int StartLine { get; set; }

        [JsonProperty("endLine")]
        public int EndLine { get; set; }

        [JsonProperty("startOffset")]
        public int StartOffset { get; set; }

        [JsonProperty("endOffset")]
        public int EndOffset { get; set; }
    }
}
