using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class QpNode
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "promotion_id")]
        public string PromotionId { get; set; }

        [DataMember(Name = "max_impressions")]
        public int? MaxImpressions { get; set; }

        [DataMember(Name = "triggers")]
        public string[] Triggers { get; set; }

        [DataMember(Name = "contextual_filters")]
        public ContextualFilters ContextualFilters { get; set; }

        [DataMember(Name = "template")]
        public Template Template { get; set; }

        [DataMember(Name = "creatives")]
        public Creative[] Creatives { get; set; }
    }
}