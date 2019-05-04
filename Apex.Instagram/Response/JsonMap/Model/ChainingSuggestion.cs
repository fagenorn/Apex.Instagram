using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class ChainingSuggestion : User
    {
        [DataMember(Name = "chaining_info")]
        public ChainingInfo ChainingInfo { get; set; }

        [DataMember(Name = "profile_chaining_secondary_label")]
        public dynamic ProfileChainingSecondaryLabel { get; set; }
    }
}