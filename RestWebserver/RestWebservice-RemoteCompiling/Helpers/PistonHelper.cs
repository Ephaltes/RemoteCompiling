using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.JsonObjClasses;

namespace RestWebservice_RemoteCompiling.Helpers
{
    public interface IPistonHelper
    {
        public Task<List<SupportedLanguages>> GetSupportedRuntimes();
        public string GetCompileTimeout();
        public string GetRunTimeout();
        public string Get_Piston_Service_Adress();

        public string DefaultVersion(string language);
    }

    public class PistonHelper : IPistonHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpHelper _httpHelper;
        public PistonHelper(IConfiguration configuration, IHttpHelper helper)
        {
            _configuration = configuration;
            _httpHelper = helper;
        }

        public async Task<List<SupportedLanguages>> GetSupportedRuntimes()
        {
            var result = await _httpHelper.ExecuteGet("api/v2/runtimes");
            var content = JsonConvert.DeserializeObject<List<SupportedLanguages>>(result);

            return content;
        }
        public string GetCompileTimeout()
        {
            return _configuration.GetSection("compile_timeout").Value;
        }
        public string GetRunTimeout()
        {
            return _configuration.GetSection("run_timeout").Value;
        }
        public string Get_Piston_Service_Adress()
        {
            return _configuration.GetSection("RemoteCompilerApiLocation").Value;
        }
        public string DefaultVersion(string language)
        {
            DefaultVersions defaultVersions = new DefaultVersions();
            _configuration.GetSection("DefaultVersions").Bind(defaultVersions);

            return defaultVersions.Versions.ContainsKey(language) ? defaultVersions.Versions[language] : null;
        }
    }
}