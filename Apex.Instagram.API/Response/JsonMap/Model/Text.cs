using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Text
    {
        [DataMember(Name = "text")]
        public string Content { get; set; }
    }
}