using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RestWebservice_StaticCodeAnalysis.DTOs.Enums;

namespace RestWebservice_StaticCodeAnalysis.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class IssueDto
    {

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [Required]
        [DataMember(Name = "type")]
        public IssueType? Type { get; set; }

        /// <summary>
        /// Gets or Sets Severity
        /// </summary>
        [Required]
        [DataMember(Name = "severity")]
        public IssueSeverity? Severity { get; set; }

        /// <summary>
        /// Gets or Sets Component
        /// </summary>
        [Required]
        [DataMember(Name = "component")]
        public string Component { get; set; }

        /// <summary>
        /// Gets or Sets TextLocation
        /// </summary>
        [Required]
        [DataMember(Name = "textLocation")]
        public TextLocationDto TextLocation { get; set; }
    }
}
