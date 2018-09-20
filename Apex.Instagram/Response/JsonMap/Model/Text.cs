using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Text
    {
        [DataMember(Name = "text")]
        public string Content { get; set; }
    }
}