using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RestWebservice_StaticCodeAnalysis.DTOs.Enums
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IssueTypeEnum
    {
        /// <summary>
        /// Enum CodeSmellEnum for CodeSmell
        /// </summary>
        [EnumMember(Value = "CodeSmell")]
        CodeSmell = 0,
        /// <summary>
        /// Enum BugsEnum for Bugs
        /// </summary>
        [EnumMember(Value = "Bugs")]
        Bugs = 1,
        /// <summary>
        /// Enum VulnerabilitiesEnum for Vulnerabilities
        /// </summary>
        [EnumMember(Value = "Vulnerabilities")]
        Vulnerabilities = 2
    }
}
