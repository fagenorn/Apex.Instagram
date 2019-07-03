using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Aymf
    {
        [DataMember(Name = "items")]
        public AymfItem[] Items { get; set; }

        [DataMember(Name = "more_available")]
        public dynamic MoreAvailable { get; set; }
    }
}