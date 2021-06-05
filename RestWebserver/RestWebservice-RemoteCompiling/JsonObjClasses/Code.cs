using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class Code
    {
        public List<string> args { get; set; } = new List<string>();
        public string stdin { get; set; } //leave "" if no input
        public string mainFile { get; set; }
        public List<FileArray> files { get; set; } = new List<FileArray>();
    }
}
