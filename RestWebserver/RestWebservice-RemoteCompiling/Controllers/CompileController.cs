using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWebservice_RemoteCompiling.JsonObjClasses;
using System.Web;
using System.Net.Http;
using RestWebservice_RemoteCompiling.Helpers;
using System.Text;
using Newtonsoft.Json;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [ApiController]
    [Route("/Api/Compile")]
    public class CompileController : ControllerBase
    {
        private readonly IPistonHelper _PistonHelper;
        private readonly HttpClient _Http;
        public CompileController(HttpClient http, IPistonHelper pistonHelper)
        {
            _Http = http;
            _PistonHelper = pistonHelper;
        }
        [HttpPost("{language}/{version}")]
        public async Task<IActionResult> ExecuteCodeWithVersion(string language, string version, JSON_Code code)
        {
            try
            {
                JSON_sendCompileRequest request = GetJsonRequestObj(language, version, code);

                HttpContent httpContent = JsonObjToHttpContent(request);

                var response =  _Http.PostAsync($"{_PistonHelper.Get_Piston_Service_Adress()}/jobs", httpContent).Result;
               
                if (response.IsSuccessStatusCode) {
                    return Ok(JsonConvert.DeserializeObject<JSON_PistonCompileAndRUn>(await response.Content.ReadAsStringAsync()));
                }
                else
                {                 
                    return BadRequest(JsonConvert.DeserializeObject<JSON_PistonError>(await response.Content.ReadAsStringAsync()));
                }
                
            }
            catch(Exception e)
            {
                return NotFound(e.Message + e.StackTrace);
            }
          
        }
        private HttpContent JsonObjToHttpContent(JSON_sendCompileRequest request)
        {
            var stringPayload = JsonConvert.SerializeObject(request);
            return new StringContent(stringPayload, Encoding.UTF8, "application/json");
        }
        private JSON_sendCompileRequest GetJsonRequestObj(string language,string version,JSON_Code Code)
        {
            JSON_sendCompileRequest item = new JSON_sendCompileRequest
            {
                language = language,
                version = version,
                main = Code.mainFile ?? "",
                stdin = Code.stdin ?? "",
                compile_timeout = Int32.Parse(_PistonHelper.GetCompileTimeout()),
                run_timeout = Int32.Parse(_PistonHelper.GetRunTimeout())
            };
            Code.files.ForEach(x => item.files.Add(x));
            Code.args.ForEach(x => item.args.Add(x));
            return item;
        }
    }
}
