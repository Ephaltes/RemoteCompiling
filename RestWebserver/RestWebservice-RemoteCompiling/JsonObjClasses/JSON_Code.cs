using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class JSON_Code
    {
        public List<string> args { get; set; }
        public string stdin { get; set; } //leave "" if no input
        [Required]
        public string mainFile { get; set; }
        public List<JSON_FileArray> files { get; set; }
    }
}
