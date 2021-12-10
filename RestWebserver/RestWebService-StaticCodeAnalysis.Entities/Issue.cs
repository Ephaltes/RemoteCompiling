using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;

using System.ComponentModel.DataAnnotations;

namespace RestWebService_StaticCodeAnalysis.Services.Entities
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }

        public IssueType? Type { get; set; }

        public IssueSeverity? Severity { get; set; }

        public string Component { get; set; }

        public string Message { get; set; }

        public virtual TextLocation TextLocation { get; set; }
    }
}
