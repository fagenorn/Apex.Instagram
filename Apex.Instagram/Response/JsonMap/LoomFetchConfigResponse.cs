using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class LoomFetchConfigResponse : Response
    {
        [DataMember(Name = "system_control")]
        public SystemControl SystemControl { get; set; }

        [DataMember(Name = "trace_control")]
        public TraceControl TraceControl { get; set; }

        [DataMember(Name = "id")]
        public int? Id { get; set; }
    }
}