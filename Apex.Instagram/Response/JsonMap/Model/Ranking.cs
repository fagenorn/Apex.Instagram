using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Ranking
    {
        [DataMember(Name = "view_name")]
        public string ViewName { get; set; }

        [DataMember(Name = "score_map")]
        public dynamic ScoreMap { get; set; }

        [DataMember(Name = "expiration_ms")]
        public ulong ExpirationMs { get; set; }
    }
}