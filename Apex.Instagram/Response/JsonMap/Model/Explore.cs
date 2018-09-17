using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Explore
    {
        [DataMember(Name = "explanation")]
        public dynamic Explanation { get; set; }

        [DataMember(Name = "actor_id")]
        public string ActorId { get; set; }

        [DataMember(Name = "source_token")]
        public dynamic SourceToken { get; set; }
    }
}