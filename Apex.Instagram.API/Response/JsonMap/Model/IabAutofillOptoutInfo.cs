using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class IabAutofillOptoutInfo
    {
        [DataMember(Name = "domain")]
        public string Domain { get; set; }

        [DataMember(Name = "is_iab_autofill_optout")]
        public bool? IsIabAutofillOptout { get; set; }
    }
}