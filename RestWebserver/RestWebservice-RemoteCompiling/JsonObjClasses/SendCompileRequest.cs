using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class SendCompileRequest
    {
        public string language { get; set; }
        public string version { get; set; }
        public List<FileArray> files { get; set; } = new List<FileArray>();
        public string stdin { get; set; }
        public List<string> args { get; set; } = new List<string>();
        public int compile_timeout { get; set; }
        public int run_timeout { get; set; }
        
    }

}
