using RestWebService_StaticCodeAnalysis.Entities.Enums;

using System.Runtime.Serialization;

namespace RestWebService_StaticCodeAnalysis.Entities
{
    public class ScanJob
    {
        public int? Id { get; set; }

        public ScanStatus? Status { get; set; }
    }
}
