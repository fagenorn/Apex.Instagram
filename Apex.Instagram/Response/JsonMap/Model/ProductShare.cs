using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class ProductShare
    {
        [DataMember(Name = "media")]
        public Item Media { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "product")]
        public Product Product { get; set; }
    }
}