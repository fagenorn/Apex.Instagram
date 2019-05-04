using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class FelixShare
    {
        [DataMember(Name = "video")]
        public Item[] Video { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}