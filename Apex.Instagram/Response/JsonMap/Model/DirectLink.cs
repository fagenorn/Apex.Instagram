using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class DirectLink
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "link_context")]
        public LinkContext LinkContext { get; set; }
    }
}