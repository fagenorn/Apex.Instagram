using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Owner
    {
        [DataMember(Name = "type")]
        public dynamic Type { get; set; }

        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "profile_pic_url")]
        public string ProfilePicUrl { get; set; }

        [DataMember(Name = "profile_pic_username")]
        public string ProfilePicUsername { get; set; }

        [DataMember(Name = "short_name")]
        public string ShortName { get; set; }

        [DataMember(Name = "lat")]
        public double? Lat { get; set; }

        [DataMember(Name = "lng")]
        public double? Lng { get; set; }

        [DataMember(Name = "location_dict")]
        public Location LocationDict { get; set; }
    }
}