using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RestWebservice_StaticCodeAnalysis.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CodeDto
    {
        /// <summary>
        /// Gets or Sets CodeLanguage
        /// </summary>
        [Required]
        [DataMember(Name = "codeLanguage")]
        public string CodeLanguage { get; set; }

        /// <summary>
        /// Gets or Sets _Code
        /// </summary>
        [Required]
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}
