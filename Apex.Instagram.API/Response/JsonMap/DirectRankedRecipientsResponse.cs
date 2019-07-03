using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class DirectRankedRecipientsResponse : Response
    {
        [DataMember(Name = "expires")]
        public dynamic Expires { get; set; }

        [DataMember(Name = "ranked_recipients")]
        public DirectRankedRecipient[] RankedRecipients { get; set; }

        [DataMember(Name = "filtered")]
        public dynamic Filtered { get; set; }

        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }

        [DataMember(Name = "rank_token")]
        public string RankToken { get; set; }
    }
}