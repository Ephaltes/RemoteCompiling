using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class JSON_Code
    {
        public List<string> args { get; set; }        
        public string stdin { get; set; } //leave "" if no input
        [Required] 
        public string mainFile { get; set; }
        public List<JSON_FileArray> files {get;set;}
    }
}
