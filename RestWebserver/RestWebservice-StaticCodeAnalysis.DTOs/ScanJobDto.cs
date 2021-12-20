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
    public class ScanJobDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        [DataMember(Name = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [Required]
        [DataMember(Name = "status")]
        public ScanStatus? Status { get; set; }
    }
}
