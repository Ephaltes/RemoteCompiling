using RestWebService_StaticCodeAnalysis.Entities.Enums;

namespace RestWebService_StaticCodeAnalysis.Entities
{
    public class Issue
    {
        public IssueType? Type { get; set; }

        public IssueSeverity? Severity  { get; set; }

        public string? Component { get; set; }

        public string? Message { get; set; }

        public TextLocation TextLocation { get; set; }
    }
}
