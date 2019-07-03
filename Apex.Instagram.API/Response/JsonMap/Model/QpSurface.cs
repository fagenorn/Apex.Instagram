using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class QpSurface
    {
        [DataMember(Name = "surface_id")]
        public int? SurfaceId { get; set; }

        [DataMember(Name = "cooldown")]
        public int? Cooldown { get; set; }
    }
}