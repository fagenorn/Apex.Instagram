using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class ChallengeResponse : Response
    {
        [DataMember(Name = "user_id")]
        public ulong? UserId { get; set; }

        [DataMember(Name = "step_name")]
        public string StepName { get; set; }

        [DataMember(Name = "step_data")]
        public StepData StepData { get; set; }

        [DataMember(Name = "nonce_code")]
        public string NonceCode { get; set; }

        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "auto_login")]
        public bool? AutoLogin { get; set; }
    }
}