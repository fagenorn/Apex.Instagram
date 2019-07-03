using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class ProductTags
    {
        [DataMember(Name = "in")]
        public In[] In { get; set; }
    }
}