using Newtonsoft.Json;

using System.Collections.Generic;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class Flow
    {
        [JsonProperty("locations")]
        public List<Location> Locations { get; set; }
    }
}
