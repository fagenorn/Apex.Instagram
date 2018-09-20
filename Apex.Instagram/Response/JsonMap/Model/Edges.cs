using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Edges
    {
        [DataMember(Name = "priority")]
        public int? Priority { get; set; }

        [DataMember(Name = "time_range")]
        public TimeRange TimeRange { get; set; }

        [DataMember(Name = "node")]
        public QpNode Node { get; set; }
    }
}