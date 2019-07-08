using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class TraceControl
    {
        [DataMember(Name = "max_trace_timeout_ms")]
        public int? MaxTraceTimeoutMs { get; set; }

        [DataMember(Name = "cold_start")]
        public dynamic ColdStart { get; set; }

        [DataMember(Name = "timed_out_upload_sample_rate")]
        public int? TimedOutUploadSampleRate { get; set; }

        [DataMember(Name = "qpl")]
        public dynamic Qpl { get; set; }
    }
}