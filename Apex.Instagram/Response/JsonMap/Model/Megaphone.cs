using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Megaphone
    {
        [DataMember(Name = "generic_megaphone")]
        public GenericMegaphone GenericMegaphone { get; set; }
    }
}