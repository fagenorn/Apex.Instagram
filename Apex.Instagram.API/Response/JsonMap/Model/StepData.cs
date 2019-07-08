using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class StepData
    {
        [DataMember(Name = "choice")]
        public string Choice { get; set; }

        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "phone_number_formatted")]
        public string PhoneNumberFormatted { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "fb_access_token")]
        public string FbAccessToken { get; set; }

        [DataMember(Name = "big_blue_token")]
        public string BigBlueToken { get; set; }

        [DataMember(Name = "google_oauth_token")]
        public string GoogleOauthToken { get; set; }

        [DataMember(Name = "security_code")]
        public string SecurityCode { get; set; }

        [DataMember(Name = "sms_resend_delay")]
        public int? SmsResendDelay { get; set; }

        [DataMember(Name = "resend_delay")]
        public int? ResendDelay { get; set; }

        [DataMember(Name = "contact_point")]
        public string ContactPoint { get; set; }

        [DataMember(Name = "form_type")]
        public string FormType { get; set; }

        [DataMember(Name = "phone_number_preview")]
        public string PhoneNumberPreview { get; set; }
    }
}