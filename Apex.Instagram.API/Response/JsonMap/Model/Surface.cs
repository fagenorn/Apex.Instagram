using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Surface
    {
        [DataMember(Name = "scores")]
        public dynamic Scores { get; set; }

        [DataMember(Name = "rank_token")]
        public string RankToken { get; set; }

        [DataMember(Name = "ttl_secs")]
        public int? TtlSecs { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}