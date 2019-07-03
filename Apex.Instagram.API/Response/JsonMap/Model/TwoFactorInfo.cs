using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class TwoFactorInfo
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "two_factor_identifier")]
        public string TwoFactorIdentifier { get; set; }

        [DataMember(Name = "phone_verification_settings")]
        public PhoneVerificationSettings PhoneVerificationSettings { get; set; }

        [DataMember(Name = "obfuscated_phone_number")]
        public dynamic ObfuscatedPhoneNumber { get; set; }
    }
}