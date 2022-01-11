using System;

namespace RestWebService_StaticCodeAnalysis.Services
{
    public class LanguageNotSupportedException : Exception
    {
        public LanguageNotSupportedException()
        {
        }

        public LanguageNotSupportedException(string message) : base(message)
        {
        }

        public LanguageNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}