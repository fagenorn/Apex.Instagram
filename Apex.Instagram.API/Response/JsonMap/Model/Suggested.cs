using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Suggested
    {
        [DataMember(Name = "position")]
        public int? Position { get; set; }

        [DataMember(Name = "hashtag")]
        public Hashtag Hashtag { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "place")]
        public LocationItem Place { get; set; }

        [DataMember(Name = "client_time")]
        public dynamic ClientTime { get; set; }
    }
}