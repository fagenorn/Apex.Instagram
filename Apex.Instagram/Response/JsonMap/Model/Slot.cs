using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Slot
    {
        [DataMember(Name = "slot")]
        public string SlotNum { get; set; }

        [DataMember(Name = "cooldown")]
        public int? Cooldown { get; set; }
    }
}