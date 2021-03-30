using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using Newtonsoft.Json;

namespace RestWebservice_RemoteCompiling.Helpers
{
    public interface IPistonHelper
    {
        public Task<List<JSON_SupportedLanguages>> GetSupportedRuntimes();
        public string GetCompileTimeout();
        public string GetRunTimeout();
        public string Get_Piston_Service_Adress();
    }
    public class PistonHelper : IPistonHelper
    {
        private readonly IConfiguration _Configuration;
        private readonly HttpClient _Http;

        public PistonHelper(IConfiguration configuration, HttpClient http)
        {
            _Configuration = configuration;
            _Http = http;
        }
        //
        public async Task<List<JSON_SupportedLanguages>> GetSupportedRuntimes()
        {
            var result = _Http.GetAsync($"{Get_Piston_Service_Adress()}/runtimes");
            var content = JsonConvert.DeserializeObject<List<JSON_SupportedLanguages>>(await result.Result.Content.ReadAsStringAsync());
            return content;
        }
        public string GetCompileTimeout()
        {
            return _Configuration.GetSection("compile_timeout").Value;
        }
        public string GetRunTimeout()
        {
            return _Configuration.GetSection("run_timeout").Value;
        }
        public string Get_Piston_Service_Adress()
        {
            return _Configuration.GetSection("RemoteCompilerApiLocation").Value;
        }
    }
}
