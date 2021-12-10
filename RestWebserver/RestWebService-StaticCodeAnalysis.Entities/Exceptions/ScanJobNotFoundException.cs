using System;

namespace RestWebService_StaticCodeAnalysis.Services.Entities.Exceptions
{
    public class ScanJobNotFoundException : Exception
    {
        public ScanJobNotFoundException()
        {
        }

        public ScanJobNotFoundException(string message) : base(message)
        {
        }

        public ScanJobNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
