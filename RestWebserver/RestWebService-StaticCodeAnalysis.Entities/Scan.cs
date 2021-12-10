
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestWebService_StaticCodeAnalysis.Services.Entities
{
    public class Scan
    {
        [Key]
        public int Id { get; set; }

        public int Total => Issues.Count;

        public virtual List<Issue> Issues { get; set; } = new List<Issue>();
    }
}
