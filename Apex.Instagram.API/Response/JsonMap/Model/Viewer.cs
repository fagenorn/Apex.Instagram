using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Viewer
    {
        [DataMember(Name = "eligible_promotions")]
        public EligiblePromotions EligiblePromotions { get; set; }
    }
}