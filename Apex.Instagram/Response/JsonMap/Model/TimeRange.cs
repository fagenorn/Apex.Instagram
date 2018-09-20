using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class TimeRange
    {
        [DataMember(Name = "start")]
        public string Start { get; set; }

        [DataMember(Name = "end")]
        public string End { get; set; }
    }
}