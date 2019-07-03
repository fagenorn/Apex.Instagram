using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Ad4Ad
    {
        [DataMember(Name = "type")]
        public dynamic Type { get; set; }

        [DataMember(Name = "title")]
        public dynamic Title { get; set; }

        [DataMember(Name = "media")]
        public Item Media { get; set; }

        [DataMember(Name = "footer")]
        public dynamic Footer { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "tracking_token")]
        public string TrackingToken { get; set; }
    }
}