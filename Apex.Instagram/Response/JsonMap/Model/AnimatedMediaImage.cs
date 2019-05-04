using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class AnimatedMediaImage
    {
        [DataMember(Name = "fixed_height")]
        public AnimatedMediaImageFixedHeigth FixedHeight { get; set; }
    }
}