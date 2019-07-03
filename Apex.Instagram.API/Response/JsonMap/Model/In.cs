using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class In
    {
        [DataMember(Name = "position")]
        public double[] Position { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "time_in_video")]
        public dynamic TimeInVideo { get; set; }

        [DataMember(Name = "start_time_in_video_in_sec")]
        public dynamic StartTimeInVideoInSec { get; set; }

        [DataMember(Name = "duration_in_video_in_sec")]
        public dynamic DurationInVideoInSec { get; set; }

        [DataMember(Name = "product")]
        public Product Product { get; set; }
    }
}