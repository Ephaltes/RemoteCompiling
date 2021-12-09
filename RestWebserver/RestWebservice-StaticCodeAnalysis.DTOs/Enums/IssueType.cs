using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RestWebservice_StaticCodeAnalysis.DTOs.Enums
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IssueType
    {
        /// <summary>
        /// Enum CodeSmellEnum for CodeSmell
        /// </summary>
        [EnumMember(Value = "CodeSmell")]
        CodeSmell = 0,
        /// <summary>
        /// Enum BugsEnum for Bugs
        /// </summary>
        [EnumMember(Value = "Bug")]
        Bug = 1,
        /// <summary>
        /// Enum VulnerabilitiesEnum for Vulnerabilities
        /// </summary>
        [EnumMember(Value = "Vulnerability")]
        Vulnerability = 2
    }
}
