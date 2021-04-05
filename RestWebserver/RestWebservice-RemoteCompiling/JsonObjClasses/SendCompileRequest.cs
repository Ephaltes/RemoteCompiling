using System.Collections.Generic;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class SendCompileRequest
    {
        public string language { get; set; }
        public string version { get; set; }
        public List<FileArray> files { get; set; }
        public string main { get; set; }
        public string stdin { get; set; }
        public List<string> args { get; set; }
        public int compile_timeout { get; set; }
        public int run_timeout { get; set; }

        public SendCompileRequest()
        {
            files = new List<FileArray>();
            args = new List<string>();
        }
    }

}
