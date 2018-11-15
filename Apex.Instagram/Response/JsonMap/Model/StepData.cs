using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class StepData

    {
        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "sms_resend_delay")]
        public int? SmsResendDelay { get; set; }

        [DataMember(Name = "phone_number_preview")]
        public string PhoneNumberPreview { get; set; }

        [DataMember(Name = "resend_delay")]
        public int? ResendDelay { get; set; }

        [DataMember(Name = "contact_point")]
        public string ContactPoint { get; set; }

        [DataMember(Name = "form_type")]
        public string FormType { get; set; }

        [DataMember(Name = "phone_number_formatted")]
        public string PhoneNumberFormatted { get; set; }
    }
}