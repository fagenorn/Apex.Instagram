using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class MultipleAccountFamilyResponse : Response
    {
        [DataMember(Name = "child_accounts")]
        public string[] ChildAccounts { get; set; }

        [DataMember(Name = "main_accounts")]
        public string[] MainAccounts { get; set; }
    }
}