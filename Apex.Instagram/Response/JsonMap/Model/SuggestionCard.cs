using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class SuggestionCard
    {
        [DataMember(Name = "user_card")]
        public UserCard UserCard { get; set; }

        [DataMember(Name = "upsell_ci_card")]
        public dynamic UpsellCiCard { get; set; }

        [DataMember(Name = "upsell_fbc_card")]
        public dynamic UpsellFbcCard { get; set; }
    }
}