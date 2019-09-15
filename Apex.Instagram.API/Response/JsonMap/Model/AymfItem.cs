using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class AymfItem : Item
    {
        [DataMember(Name = "caption")]
        [JsonIgnore]
        public new string Caption { get; set; }

        [DataMember(Name = "uuid")]
        public string Uuid { get; set; }
    }
}