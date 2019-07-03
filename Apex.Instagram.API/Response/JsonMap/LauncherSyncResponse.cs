using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class LauncherSyncResponse : Response
    {
        [DataMember(Name = "configs")]
        public dynamic Configs { get; set; }
    }
}