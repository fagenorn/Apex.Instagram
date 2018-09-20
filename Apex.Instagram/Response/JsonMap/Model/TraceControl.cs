using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class TraceControl
    {
        [DataMember(Name = "max_trace_timeout_ms")]
        public int? MaxTraceTimeoutMs { get; set; }
    }
}