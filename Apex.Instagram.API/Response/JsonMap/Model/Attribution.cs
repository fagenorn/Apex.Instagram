using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Attribution
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}