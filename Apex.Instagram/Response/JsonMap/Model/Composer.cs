using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Composer
    {
        [DataMember(Name = "nux_finished")]
        public bool? NuxFinished { get; set; }

        [DataMember(Name = "aspect_ratio_finished")]
        public bool? AspectRatioFinished { get; set; }
    }
}