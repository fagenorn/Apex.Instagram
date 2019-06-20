using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class QpCooldownsResponse : Response
    {
        [DataMember(Name = "global")]
        public int? Global { get; set; }

        [DataMember(Name = "default")]
        public int? Default { get; set; }

        [DataMember(Name = "surfaces")]
        public QpSurface[] Surfaces { get; set; }

        [DataMember(Name = "slots")]
        public Slot[] Slots { get; set; }
    }
}