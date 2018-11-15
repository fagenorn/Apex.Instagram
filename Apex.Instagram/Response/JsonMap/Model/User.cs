using System;
using System.Runtime.Serialization;

using Utf8Json;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class User
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "has_anonymous_profile_picture")]
        public bool? HasAnonymousProfilePicture { get; set; }

        [DataMember(Name = "has_highlight_reels")]
        public bool? HasHighlightReels { get; set; }

        [DataMember(Name = "is_favorite")]
        public bool? IsFavorite { get; set; }

        [DataMember(Name = "is_favorite_for_stories")]
        public bool? IsFavoriteForStories { get; set; }

        [DataMember(Name = "is_favorite_for_highlights")]
        public bool? IsFavoriteForHighlights { get; set; }

        [DataMember(Name = "can_be_reported_as_fraud")]
        public bool? CanBeReportedAsFraud { get; set; }

        [DataMember(Name = "profile_pic_url")]
        public Uri ProfilePicUrl { get; set; }

        [DataMember(Name = "profile_pic_id")]
        public string ProfilePicId { get; set; }

        [DataMember(Name = "permission")]
        public bool? Permission { get; set; }

        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "id")]
        public ulong? Id { get; set; }

        [DataMember(Name = "is_verified")]
        public bool? IsVerified { get; set; }

        [DataMember(Name = "is_private")]
        public bool? IsPrivate { get; set; }

        [DataMember(Name = "coeff_weight")]
        public dynamic CoeffWeight { get; set; }

        [DataMember(Name = "friendship_status")]
        public FriendshipStatus FriendshipStatus { get; set; }

        [DataMember(Name = "hd_profile_pic_versions")]
        public ImageCandidate[] HdProfilePicVersions { get; set; }

        [DataMember(Name = "byline")]
        public dynamic Byline { get; set; }

        [DataMember(Name = "search_social_context")]
        public dynamic SearchSocialContext { get; set; }

        [DataMember(Name = "unseen_count")]
        public dynamic UnseenCount { get; set; }

        [DataMember(Name = "mutual_followers_count")]
        public int? MutualFollowersCount { get; set; }

        [DataMember(Name = "follower_count")]
        public int? FollowerCount { get; set; }

        [DataMember(Name = "search_subtitle")]
        public string SearchSubtitle { get; set; }

        [DataMember(Name = "social_context")]
        public string SocialContext { get; set; }

        [DataMember(Name = "media_count")]
        public int? MediaCount { get; set; }

        [DataMember(Name = "following_count")]
        public int? FollowingCount { get; set; }

        [DataMember(Name = "following_tag_count")]
        public int? FollowingTagCount { get; set; }

        [DataMember(Name = "is_business")]
        public bool? IsBusiness { get; set; }

        [DataMember(Name = "usertags_count")]
        public int? UsertagsCount { get; set; }

        [DataMember(Name = "profile_context")]
        public dynamic ProfileContext { get; set; }

        [DataMember(Name = "biography")]
        public string Biography { get; set; }

        [DataMember(Name = "geo_media_count")]
        public int? GeoMediaCount { get; set; }

        [DataMember(Name = "is_unpublished")]
        public bool? IsUnpublished { get; set; }

        [DataMember(Name = "allow_contacts_sync")]
        public dynamic AllowContactsSync { get; set; }

        [DataMember(Name = "show_feed_biz_conversion_icon")]
        public dynamic ShowFeedBizConversionIcon { get; set; }

        [DataMember(Name = "auto_expand_chaining")]
        public dynamic AutoExpandChaining { get; set; }

        [DataMember(Name = "can_boost_post")]
        public dynamic CanBoostPost { get; set; }

        [DataMember(Name = "is_profile_action_needed")]
        public bool? IsProfileActionNeeded { get; set; }

        [DataMember(Name = "has_chaining")]
        public bool? HasChaining { get; set; }

        [DataMember(Name = "chaining_suggestions")]
        public ChainingSuggestion[] ChainingSuggestions { get; set; }

        [DataMember(Name = "include_direct_blacklist_status")]
        public dynamic IncludeDirectBlacklistStatus { get; set; }

        [DataMember(Name = "can_see_organic_insights")]
        public bool? CanSeeOrganicInsights { get; set; }

        [DataMember(Name = "has_placed_orders")]
        public bool? HasPlacedOrders { get; set; }

        [DataMember(Name = "can_convert_to_business")]
        public bool? CanConvertToBusiness { get; set; }

        [DataMember(Name = "convert_from_pages")]
        public dynamic ConvertFromPages { get; set; }

        [DataMember(Name = "show_business_conversion_icon")]
        public bool? ShowBusinessConversionIcon { get; set; }

        [DataMember(Name = "show_conversion_edit_entry")]
        public bool? ShowConversionEditEntry { get; set; }

        [DataMember(Name = "show_insights_terms")]
        public bool? ShowInsightsTerms { get; set; }

        [DataMember(Name = "can_create_sponsor_tags")]
        public dynamic CanCreateSponsorTags { get; set; }

        [DataMember(Name = "hd_profile_pic_url_info")]
        public ImageCandidate HdProfilePicUrlInfo { get; set; }

        [DataMember(Name = "usertag_review_enabled")]
        public dynamic UsertagReviewEnabled { get; set; }

        [DataMember(Name = "profile_context_mutual_follow_ids")]
        public ulong[] ProfileContextMutualFollowIds { get; set; }

        [DataMember(Name = "profile_context_links_with_user_ids")]
        public Link[] ProfileContextLinksWithUserIds { get; set; }

        [DataMember(Name = "has_biography_translation")]
        public bool? HasBiographyTranslation { get; set; }

        [DataMember(Name = "can_link_entities_in_bio")]
        public bool? CanLinkEntitiesInBio { get; set; }

        [DataMember(Name = "biography_with_entities")]
        public BiographyEntities BiographyWithEntities { get; set; }

        [DataMember(Name = "max_num_linked_entities_in_bio")]
        public int? MaxNumLinkedEntitiesInBio { get; set; }

        [DataMember(Name = "business_contact_method")]
        public string BusinessContactMethod { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "direct_messaging")]
        public string DirectMessaging { get; set; }

        [DataMember(Name = "page_name")]
        public dynamic PageName { get; set; }

        [DataMember(Name = "fb_page_call_to_action_id")]
        public string FbPageCallToActionId { get; set; }

        [DataMember(Name = "is_call_to_action_enabled")]
        public bool? IsCallToActionEnabled { get; set; }

        [DataMember(Name = "public_phone_country_code")]
        public string PublicPhoneCountryCode { get; set; }

        [DataMember(Name = "public_phone_number")]
        public string PublicPhoneNumber { get; set; }

        [DataMember(Name = "contact_phone_number")]
        public string ContactPhoneNumber { get; set; }

        [DataMember(Name = "latitude")]
        public double? Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double? Longitude { get; set; }

        [DataMember(Name = "address_street")]
        public string AddressStreet { get; set; }

        [DataMember(Name = "zip")]
        public string Zip { get; set; }

        [DataMember(Name = "city_id")]
        public ulong? CityId { get; set; }

        [DataMember(Name = "city_name")]
        public string CityName { get; set; }

        [DataMember(Name = "public_email")]
        public string PublicEmail { get; set; }

        [DataMember(Name = "is_needy")]
        public bool? IsNeedy { get; set; }

        [DataMember(Name = "external_url")]
        public Uri ExternalUrl { get; set; }

        [DataMember(Name = "external_lynx_url")]
        public Uri ExternalLynxUrl { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "country_code")]
        public int? CountryCode { get; set; }

        [DataMember(Name = "birthday")]
        public dynamic Birthday { get; set; }

        [DataMember(Name = "national_number")]
        public ulong? NationalNumber { get; set; }

        [DataMember(Name = "gender")]
        public int? Gender { get; set; }

        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "needs_email_confirm")]
        public dynamic NeedsEmailConfirm { get; set; }

        [DataMember(Name = "is_active")]
        public bool? IsActive { get; set; }

        [DataMember(Name = "block_at")]
        public dynamic BlockAt { get; set; }

        [DataMember(Name = "aggregate_promote_engagement")]
        public dynamic AggregatePromoteEngagement { get; set; }

        [DataMember(Name = "fbuid")]
        public dynamic Fbuid { get; set; }

        [DataMember(Name = "page_id")]
        public ulong? PageId { get; set; }

        [DataMember(Name = "can_claim_page")]
        public bool? CanClaimPage { get; set; }

        [DataMember(Name = "fb_page_call_to_action_ix_app_id")]
        public int? FbPageCallToActionIxAppId { get; set; }

        [DataMember(Name = "fb_page_call_to_action_ix_url")]
        public Uri FbPageCallToActionIxUrl { get; set; }

        [DataMember(Name = "can_crosspost_without_fb_token")]
        public bool? CanCrosspostWithoutFbToken { get; set; }

        [DataMember(Name = "fb_page_call_to_action_ix_partner")]
        public string FbPageCallToActionIxPartner { get; set; }

        [DataMember(Name = "shoppable_posts_count")]
        public int? ShoppablePostsCount { get; set; }

        [DataMember(Name = "show_shoppable_feed")]
        public bool? ShowShoppableFeed { get; set; }

        [DataMember(Name = "latest_reel_media")]
        public ulong? LatestReelMedia { get; set; }

        [DataMember(Name = "has_unseen_besties_media")]
        public bool? HasUnseenBestiesMedia { get; set; }

        [DataMember(Name = "allowed_commenter_type")]
        public string AllowedCommenterType { get; set; }

        [DataMember(Name = "reel_auto_archive")]
        public string ReelAutoArchive { get; set; }

        [DataMember(Name = "is_directapp_installed")]
        public bool? IsDirectappInstalled { get; set; }

        [DataMember(Name = "besties_count")]
        public int? BestiesCount { get; set; }

        [DataMember(Name = "can_be_tagged_as_sponsor")]
        public bool? CanBeTaggedAsSponsor { get; set; }

        [DataMember(Name = "can_follow_hashtag")]
        public bool? CanFollowHashtag { get; set; }

        [DataMember(Name = "has_profile_video_feed")]
        public bool? HasProfileVideoFeed { get; set; }

        [DataMember(Name = "is_video_creator")]
        public bool? IsVideoCreator { get; set; }

        [DataMember(Name = "show_besties_badge")]
        public bool? ShowBestiesBadge { get; set; }

        [DataMember(Name = "screenshotted")]
        public bool? Screenshotted { get; set; }

        [DataMember(Name = "nametag")]
        public Nametag Nametag { get; set; }

        [DataMember(Name = "school")]
        public dynamic School { get; set; }

        [DataMember(Name = "is_bestie")]
        public bool? IsBestie { get; set; }

        public override string ToString() { return JsonSerializer.PrettyPrint(JsonSerializer.Serialize(this)); }
    }
}