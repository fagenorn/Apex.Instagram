using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap
{
    public class LinkageStatusResponse : Response
    {
        [DataMember(Name = "linkage")]
        public dynamic Linkage { get; set; }
    }
}