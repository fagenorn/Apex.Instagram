using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class SectionMedia
    {
        [DataMember(Name = "media")]
        public Item Media { get; set; }
    }
}