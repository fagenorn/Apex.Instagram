using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class CheckpointResponse : Response
    {
        [DataMember(Name = "step_name")]
        public string StepName { get; set; }

        [DataMember(Name = "challenge")]
        public Challenge Challenge { get; set; }

        [DataMember(Name = "step_data")]
        public StepData StepData { get; set; }

        [DataMember(Name = "user_id")]
        public ulong? UserId { get; set; }

        [DataMember(Name = "nonce")]
        public string Nonce { get; set; }
    }
}