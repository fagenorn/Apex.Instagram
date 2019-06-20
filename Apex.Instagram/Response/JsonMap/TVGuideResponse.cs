using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class TvGuideResponse : Response
    {
        [DataMember(Name = "channels")]
        public TvChannel[] Channels { get; set; }

        [DataMember(Name = "my_channel")]
        public TvChannel MyChannel { get; set; }

        [DataMember(Name = "badging")]
        public Badging Badging { get; set; }

        [DataMember(Name = "composer")]
        public Composer Composer { get; set; }

        [DataMember(Name = "banner_token")]
        public string BannerToken { get; set; }
    }
}