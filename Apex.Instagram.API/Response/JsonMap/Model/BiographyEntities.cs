using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class BiographyEntities
    {
        [DataMember(Name = "entities")]
        public dynamic Entities { get; set; }

        [DataMember(Name = "raw_text")]
        public string RawText { get; set; }

        [DataMember(Name = "nux_type")]
        public string NuxType { get; set; }
    }
}