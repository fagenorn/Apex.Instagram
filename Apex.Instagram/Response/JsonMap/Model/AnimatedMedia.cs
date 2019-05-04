using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class AnimatedMedia
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "images")]
        public AnimatedMediaImage Images { get; set; }
    }
}