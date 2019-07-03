using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class LocationItem
    {
        [DataMember(Name = "media_bundles")]
        public dynamic MediaBundles { get; set; }

        [DataMember(Name = "subtitle")]
        public dynamic Subtitle { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "title")]
        public dynamic Title { get; set; }
    }
}