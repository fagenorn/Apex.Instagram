using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class QpExtraInfo
    {
        [DataMember(Name = "surface")]
        public string Surface { get; set; }

        [DataMember(Name = "extra_info")]
        public string ExtraInfo { get; set; }
    }
}