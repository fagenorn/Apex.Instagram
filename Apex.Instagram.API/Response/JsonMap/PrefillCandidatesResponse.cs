using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class PrefillCandidatesResponse : Response
    {
        [DataMember(Name = "prefills")]
        public Prefill[] Prefills { get; set; }
    }
}