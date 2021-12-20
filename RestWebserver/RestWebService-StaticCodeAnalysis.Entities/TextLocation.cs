using System.ComponentModel.DataAnnotations;

namespace RestWebService_StaticCodeAnalysis.Services.Entities
{
    public class TextLocation
    {
        [Key]
        public int Id { get; set; }

        public int? StartLine { get; set; }

        public int? EndLine { get; set; }

        public int? StartOffset { get; set; }

        public int? EndOffset { get; set; }
    }
}