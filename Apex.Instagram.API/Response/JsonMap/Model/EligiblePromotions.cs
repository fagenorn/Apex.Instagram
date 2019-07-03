using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class EligiblePromotions
    {
        [DataMember(Name = "edges")]
        public Edges[] Edges { get; set; }
    }
}