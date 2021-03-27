using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestWebservice_RemoteCompiling.JsonObjClasses
{
    public class JSON_Code
    {
        [Required]
        public string CodeAsValue { get; set; }
        [Required]
        public string CompileArguments { get; set; }
    }
}
