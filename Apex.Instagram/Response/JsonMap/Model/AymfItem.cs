using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class AymfItem : Item
    {
        [DataMember(Name = "caption")]
        public new string Caption { get; set; }
    }
}