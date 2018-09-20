using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Location
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "external_id_source")]
        public string ExternalIdSource { get; set; }

        [DataMember(Name = "external_source")]
        public string ExternalSource { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "lat")]
        public double? Lat { get; set; }

        [DataMember(Name = "lng")]
        public double? Lng { get; set; }

        [DataMember(Name = "external_id")]
        public string ExternalId { get; set; }

        [DataMember(Name = "facebook_places_id")]
        public ulong? FacebookPlacesId { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "short_name")]
        public string ShortName { get; set; }

        [DataMember(Name = "facebook_events_id")]
        public string FacebookEventsId { get; set; }

        [DataMember(Name = "start_time")]
        public dynamic StartTime { get; set; }

        [DataMember(Name = "end_time")]
        public dynamic EndTime { get; set; }

        [DataMember(Name = "location_dict")]
        public Location LocationDict { get; set; }

        [DataMember(Name = "type")]
        public dynamic Type { get; set; }

        [DataMember(Name = "profile_pic_url")]
        public string ProfilePicUrl { get; set; }

        [DataMember(Name = "profile_pic_username")]
        public string ProfilePicUsername { get; set; }

        [DataMember(Name = "time_granularity")]
        public dynamic TimeGranularity { get; set; }

        [DataMember(Name = "timezone")]
        public dynamic Timezone { get; set; }

        [DataMember(Name = "country")]
        public int? Country { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "event_category")]
        public int? EventCategory { get; set; }

        [DataMember(Name = "place_fbid")]
        public string PlaceFbid { get; set; }

        [DataMember(Name = "place_name")]
        public string PlaceName { get; set; }
    }
}