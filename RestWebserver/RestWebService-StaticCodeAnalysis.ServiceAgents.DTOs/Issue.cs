using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class Issue
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("component")]
        public string Component { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("rule")]
        public string Rule { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("severity")]
        public string Severity { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("line")]
        public int Line { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("effort")]
        public string Effort { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("updateDate")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("attr")]
        public Reference Reference { get; set; }

        [JsonProperty("transitions")]
        public List<string> Transitions { get; set; }

        [JsonProperty("actions")]
        public List<string> Actions { get; set; }

        [JsonProperty("textRange")]
        public TextRange TextRange { get; set; }

        [JsonProperty("flows")]
        public List<Flow> Flows { get; set; }

        [JsonProperty("quickFixAvailable")]
        public bool QuickFixAvailable { get; set; }
    }
}
