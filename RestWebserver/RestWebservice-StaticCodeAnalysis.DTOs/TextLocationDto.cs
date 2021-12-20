using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace RestWebservice_StaticCodeAnalysis.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class TextLocationDto
    {
        /// <summary>
        /// Gets or Sets StartLine
        /// </summary>
        [Required]
        [DataMember(Name = "startLine")]
        public int? StartLine { get; set; }

        /// <summary>
        /// Gets or Sets EndLine
        /// </summary>
        [Required]
        [DataMember(Name = "endLine")]
        public int? EndLine { get; set; }

        /// <summary>
        /// Gets or Sets StartOffset
        /// </summary>
        [Required]
        [DataMember(Name = "startOffset")]
        public int? StartOffset { get; set; }

        /// <summary>
        /// Gets or Sets EndOffset
        /// </summary>
        [Required]
        [DataMember(Name = "endOffset")]
        public int? EndOffset { get; set; }
    }
}
