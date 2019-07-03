using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class HideReason
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "reason")]
        public string Reason { get; set; }
    }
}