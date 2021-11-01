using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace RestWebservice_StaticCodeAnalysis.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ScanDto
    {
        /// <summary>
        /// Gets or Sets Total
        /// </summary>
        [Required]
        [DataMember(Name = "total")]
        public int? Total { get; set; }

        /// <summary>
        /// Gets or Sets Issues
        /// </summary>
        [Required]
        [DataMember(Name = "issues")]
        public List<IssueDto> Issues { get; set; }
    }
}
