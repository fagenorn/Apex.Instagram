using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class PrefillCandidatesResponse : Response
    {
        [DataMember(Name = "prefills")]
        public Prefill[] Prefills { get; set; }
    }
}