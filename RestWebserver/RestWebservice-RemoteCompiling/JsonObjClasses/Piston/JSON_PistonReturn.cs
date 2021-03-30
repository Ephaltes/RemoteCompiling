using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class JSON_PistonReturn
    {
        public string stdout { get; set; }
        public string stderr { get; set; }
        public int code { get; set; }
        public string signal { get; set; }
    }
}
