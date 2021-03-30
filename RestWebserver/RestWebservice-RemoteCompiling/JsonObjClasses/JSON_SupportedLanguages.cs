using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class JSON_SupportedLanguages
    {
        public string language { get; set; }
        public string version { get; set; }
        public List<string> aliases { get; set; }

        JSON_SupportedLanguages()
        {
            
        }
    }
}
