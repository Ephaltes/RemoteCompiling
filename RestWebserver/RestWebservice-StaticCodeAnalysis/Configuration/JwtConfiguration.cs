

using RestWebservice_StaticCodeAnalysis.Interfaces;

namespace RestWebservice_StaticCodeAnalysis.Configuration
{
    public class JwtConfiguration : IJwtConfiguration
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpiresInSeconds { get; set; }
    }
}
