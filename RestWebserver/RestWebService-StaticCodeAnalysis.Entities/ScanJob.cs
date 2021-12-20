using RestWebService_StaticCodeAnalysis.Services.Entities.Enums;

using System.ComponentModel.DataAnnotations;

namespace RestWebService_StaticCodeAnalysis.Services.Entities
{
    public class ScanJob
    {
        [Key]
        public int Id { get; set; }

        public ScanStatus? Status { get; set; }

        public virtual Scan? Scan { get; set; }
    }
}
