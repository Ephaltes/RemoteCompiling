using Newtonsoft.Json;

using System;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class Comment
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("htmlText")]
        public string HtmlText { get; set; }

        [JsonProperty("markdown")]
        public string Markdown { get; set; }

        [JsonProperty("updatable")]
        public bool Updatable { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
