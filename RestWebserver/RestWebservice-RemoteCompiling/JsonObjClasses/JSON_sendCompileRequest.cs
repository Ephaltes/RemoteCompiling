using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class JSON_sendCompileRequest
    {
        public string language { get; set; }
        public string version { get; set; }
        public List<JSON_FileArray> files{ get; set; }
        public string main { get; set; }
        public string stdin { get; set; }
        public List<string> args { get; set; }
        public int compile_timeout { get; set; }
        public int run_timeout { get; set; }
   
        public JSON_sendCompileRequest()
    {
            files = new List<JSON_FileArray>();
            args = new List<string>();
    }
}

}
