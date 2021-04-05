using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestWebservice_RemoteCompiling.Helpers
{
    public interface IPistonHelper
    {
        public Task<List<SupportedLanguages>> GetSupportedRuntimes();
        public string GetCompileTimeout();
        public string GetRunTimeout();
        public string Get_Piston_Service_Adress();
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
        //
        public async Task<List<SupportedLanguages>> GetSupportedRuntimes()
        {
            var result = await _httpHelper.ExecuteGet("runtimes");
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
    }
}
