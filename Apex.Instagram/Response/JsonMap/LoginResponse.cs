using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class LoginResponse : Response
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "has_anonymous_profile_picture")]
        public bool? HasAnonymousProfilePicture { get; set; }

        [DataMember(Name = "profile_pic_url")]
        public string ProfilePicUrl { get; set; }

        [DataMember(Name = "profile_pic_id")]
        public string ProfilePicId { get; set; }

        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "is_private")]
        public bool? IsPrivate { get; set; }

        [DataMember(Name = "is_verified")]
        public bool? IsVerified { get; set; }

        [DataMember(Name = "allowed_commenter_type")]
        public string AllowedCommenterType { get; set; }

        [DataMember(Name = "reel_auto_archive")]
        public string ReelAutoArchive { get; set; }

        [DataMember(Name = "allow_contacts_sync")]
        public bool? AllowContactsSync { get; set; }

        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "country_code")]
        public int? CountryCode { get; set; }

        [DataMember(Name = "national_number")]
        public int? NationalNumber { get; set; }

        [DataMember(Name = "error_title")]
        public dynamic ErrorTitle { get; set; }

        [DataMember(Name = "error_type")]
        public dynamic ErrorType { get; set; }

        [DataMember(Name = "buttons")]
        public dynamic Buttons { get; set; }

        [DataMember(Name = "invalid_credentials")]
        public dynamic InvalidCredentials { get; set; }

        [DataMember(Name = "logged_in_user")]
        public User LoggedInUser { get; set; }

        [DataMember(Name = "two_factor_required")]
        public dynamic TwoFactorRequired { get; set; }

        [DataMember(Name = "phone_verification_settings")]
        public PhoneVerificationSettings PhoneVerificationSettings { get; set; }

        [DataMember(Name = "two_factor_info")]
        public TwoFactorInfo TwoFactorInfo { get; set; }

        [DataMember(Name = "checkpoint_url")]
        public string CheckpointUrl { get; set; }

        [DataMember(Name = "lock")]
        public dynamic Lock { get; set; }

        [DataMember(Name = "help_url")]
        public string HelpUrl { get; set; }

        [DataMember(Name = "challenge")]
        public Challenge Challenge { get; set; }
    }
}