using Newtonsoft.Json;

namespace RestWebService_StaticCodeAnalysis.ServiceAgents.DTOs
{
    public class Paging
    {
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
