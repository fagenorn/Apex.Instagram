using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class PhoneVerificationSettings
    {
        [DataMember(Name = "resend_sms_delay_sec")]
        public int? ResendSmsDelaySec { get; set; }

        [DataMember(Name = "max_sms_count")]
        public int? MaxSmsCount { get; set; }

        [DataMember(Name = "robocall_count_down_time_sec")]
        public int? RobocallCountDownTimeSec { get; set; }

        [DataMember(Name = "robocall_after_max_sms")]
        public bool? RobocallAfterMaxSms { get; set; }
    }
}