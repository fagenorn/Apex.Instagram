using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class MediaShare
    {
        [DataMember(Name = "media")]
        public Item Media { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}