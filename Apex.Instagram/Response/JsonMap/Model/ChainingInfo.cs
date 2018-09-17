using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class ChainingInfo
    {
        [DataMember(Name = "sources")]
        public string Sources { get; set; }
    }
}