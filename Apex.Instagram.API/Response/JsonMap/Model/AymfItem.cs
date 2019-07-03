using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class AymfItem : Item
    {
        [DataMember(Name = "caption")]
        public new string Caption { get; set; }

        [DataMember(Name = "uuid")]
        public string Uuid { get; set; }
    }
}