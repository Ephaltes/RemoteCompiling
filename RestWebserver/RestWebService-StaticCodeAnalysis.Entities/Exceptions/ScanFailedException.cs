
using System;

namespace RestWebService_StaticCodeAnalysis.Services.Entities.Exceptions
{
    public class ScanFailedException : Exception
    {
        public ScanFailedException()
        {
        }

        public ScanFailedException(string message) : base(message)
        {
        }

        public ScanFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}