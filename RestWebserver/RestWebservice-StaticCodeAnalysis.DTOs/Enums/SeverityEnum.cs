using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RestWebservice_StaticCodeAnalysis.DTOs.Enums
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SeverityEnum
    {
        /// <summary>
        /// Enum MajorEnum for major
        /// </summary>
        [EnumMember(Value = "major")]
        Major = 0,
        /// <summary>
        /// Enum MinorEnum for minor
        /// </summary>
        [EnumMember(Value = "minor")]
        Minor = 1
    }
}
