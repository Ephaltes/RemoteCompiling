using System.Collections.Generic;

namespace RestWebService_StaticCodeAnalysis.Entities
{
    public class Scan
    {
        public int Total => Issues.Count;

        public List<Issue> Issues { get; set; }
    }
}
