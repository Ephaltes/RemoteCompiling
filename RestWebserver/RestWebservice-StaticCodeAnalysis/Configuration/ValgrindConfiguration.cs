

using RestWebservice_StaticCodeAnalysis.Interfaces;

namespace RestWebservice_StaticCodeAnalysis.Configuration
{
    public class ValgrindConfiguration : IValgrindConfiguration
    {
        public string Flags { get; set; }
    }
}
