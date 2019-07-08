using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class ExploreResponse : Response
    {
        [DataMember(Name = "num_results")]
        public int? NumResults { get; set; }

        [DataMember(Name = "auto_load_more_enabled")]
        public bool AutoLoadMoreEnabled { get; set; }

        [DataMember(Name = "items")]
        public ExploreItem[] Items { get; set; }

        [DataMember(Name = "sectional_items")]
        public Section[] SectionalItems { get; set; }

        [DataMember(Name = "more_available")]
        public bool MoreAvailable { get; set; }

        [DataMember(Name = "next_max_id")]
        public string NextMaxId { get; set; }

        [DataMember(Name = "max_id")]
        public string MaxId { get; set; }

        [DataMember(Name = "rank_token")]
        public string RankToken { get; set; }
    }
}