using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class ImageVersions2
    {
        [DataMember(Name = "candidates")]
        public ImageCandidate[] Candidates { get; set; }

        [DataMember(Name = "trace_token")]
        public dynamic TraceToken { get; set; }
    }
}