using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Gating
    {
        [DataMember(Name = "gating_type")]
        public dynamic GatingType { get; set; }

        [DataMember(Name = "description")]
        public dynamic Description { get; set; }

        [DataMember(Name = "buttons")]
        public dynamic Buttons { get; set; }

        [DataMember(Name = "title")]
        public dynamic Title { get; set; }
    }
}