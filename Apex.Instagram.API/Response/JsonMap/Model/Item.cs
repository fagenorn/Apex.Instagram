using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Item
    {
        public const int Photo = 1;

        public const int Video = 2;

        public const int Carousel = 8;

        public string GetItemUrl() { return $"https://www.instagram.com/p/{Code}/"; }

        public bool? IsAd() { return DrAdType != null; }

        #region Properties

        [DataMember(Name = "taken_at")]
        public ulong? TakenAt { get; set; }

        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "device_timestamp")]
        public ulong? DeviceTimestamp { get; set; }

        [DataMember(Name = "media_type")]
        public int? MediaType { get; set; }

        [DataMember(Name = "dynamic_item_id")]
        public string DynamicItemId { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "client_cache_key")]
        public string ClientCacheKey { get; set; }

        [DataMember(Name = "filter_type")]
        public int? FilterType { get; set; }

        [DataMember(Name = "product_type")]
        public string ProductType { get; set; }

        [DataMember(Name = "nearly_complete_copyright_match")]
        public bool? NearlyCompleteCopyrightMatch { get; set; }

        [DataMember(Name = "image_versions2")]
        public ImageVersions2 ImageVersions2 { get; set; }

        [DataMember(Name = "original_width")]
        public int? OriginalWidth { get; set; }

        [DataMember(Name = "original_height")]
        public int? OriginalHeight { get; set; }

        [DataMember(Name = "caption_position")]
        public double? CaptionPosition { get; set; }

        [DataMember(Name = "is_reel_media")]
        public bool? IsReelMedia { get; set; }

        [DataMember(Name = "video_versions")]
        public VideoVersions[] VideoVersions { get; set; }

        [DataMember(Name = "has_audio")]
        public bool? HasAudio { get; set; }

        [DataMember(Name = "video_duration")]
        public double? VideoDuration { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "can_see_insights_as_brand")]
        public bool? CanSeeInsightsAsBrand { get; set; }

        [DataMember(Name = "caption")]
        [JsonIgnore]
        public Caption Caption { get; set; }

        [DataMember(Name = "headline")]
        public Caption Headline { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "caption_is_edited")]
        public bool? CaptionIsEdited { get; set; }

        [DataMember(Name = "photo_of_you")]
        public bool? PhotoOfYou { get; set; }

        [DataMember(Name = "fb_user_tags")]
        public Usertag FbUserTags { get; set; }

        [DataMember(Name = "can_viewer_save")]
        public bool? CanViewerSave { get; set; }

        [DataMember(Name = "has_viewer_saved")]
        public bool? HasViewerSaved { get; set; }

        [DataMember(Name = "organic_tracking_token")]
        public string OrganicTrackingToken { get; set; }

        [DataMember(Name = "follow_hashtag_info")]
        public Hashtag FollowHashtagInfo { get; set; }

        [DataMember(Name = "expiring_at")]
        public ulong? ExpiringAt { get; set; }

        [DataMember(Name = "audience")]
        public string Audience { get; set; }

        [DataMember(Name = "is_dash_eligible")]
        public int? IsDashEligible { get; set; }

        [DataMember(Name = "video_dash_manifest")]
        public string VideoDashManifest { get; set; }

        [DataMember(Name = "number_of_qualities")]
        public int? NumberOfQualities { get; set; }

        [DataMember(Name = "video_codec")]
        public string VideoCodec { get; set; }

        [DataMember(Name = "thumbnails")]
        public Thumbnail Thumbnails { get; set; }

        [DataMember(Name = "can_reshare")]
        public bool? CanReshare { get; set; }

        [DataMember(Name = "can_reply")]
        public bool? CanReply { get; set; }

        [DataMember(Name = "is_pride_media")]
        public bool? IsPrideMedia { get; set; }

        [DataMember(Name = "can_viewer_reshare")]
        public bool? CanViewerReshare { get; set; }

        [DataMember(Name = "visibility")]
        public dynamic Visibility { get; set; }

        [DataMember(Name = "attribution")]
        public Attribution Attribution { get; set; }

        [DataMember(Name = "view_count")]
        public double? ViewCount { get; set; }

        [DataMember(Name = "viewer_count")]
        public int? ViewerCount { get; set; }

        [DataMember(Name = "comment_count")]
        public int? CommentCount { get; set; }

        [DataMember(Name = "can_view_more_preview_comments")]
        public bool? CanViewMorePreviewComments { get; set; }

        [DataMember(Name = "has_more_comments")]
        public bool? HasMoreComments { get; set; }

        [DataMember(Name = "max_num_visible_preview_comments")]
        public int? MaxNumVisiblePreviewComments { get; set; }

        [DataMember(Name = "preview_comments")]
        public Comment[] PreviewComments { get; set; }

        [DataMember(Name = "comments")]
        public Comment[] Comments { get; set; }

        [DataMember(Name = "comments_disabled")]
        public dynamic CommentsDisabled { get; set; }

        [DataMember(Name = "reel_mentions")]
        public ReelMention[] ReelMentions { get; set; }

        [DataMember(Name = "story_cta")]
        public StoryCta[] StoryCta { get; set; }

        [DataMember(Name = "next_max_id")]
        public ulong? NextMaxId { get; set; }

        [DataMember(Name = "carousel_media")]
        public CarouselMedia[] CarouselMedia { get; set; }

        [DataMember(Name = "carousel_media_type")]
        public dynamic CarouselMediaType { get; set; }

        [DataMember(Name = "carousel_media_count")]
        public int? CarouselMediaCount { get; set; }

        [DataMember(Name = "likers")]
        public User[] Likers { get; set; }

        [DataMember(Name = "facepile_top_likers")]
        public User[] FacepileTopLikers { get; set; }

        [DataMember(Name = "like_count")]
        public int? LikeCount { get; set; }

        [DataMember(Name = "preview")]
        public string Preview { get; set; }

        [DataMember(Name = "has_liked")]
        public bool? HasLiked { get; set; }

        [DataMember(Name = "explore_context")]
        public string ExploreContext { get; set; }

        [DataMember(Name = "explore_source_token")]
        public string ExploreSourceToken { get; set; }

        [DataMember(Name = "explore_hide_comments")]
        public bool? ExploreHideComments { get; set; }

        [DataMember(Name = "explore")]
        public Explore Explore { get; set; }

        [DataMember(Name = "impression_token")]
        public string ImpressionToken { get; set; }

        [DataMember(Name = "usertags")]
        public Usertag Usertags { get; set; }

        [DataMember(Name = "media")]
        public Media Media { get; set; }

        [DataMember(Name = "stories")]
        public Stories Stories { get; set; }

        [DataMember(Name = "top_likers")]
        public string[] TopLikers { get; set; }

        [DataMember(Name = "direct_reply_to_author_enabled")]
        public bool? DirectReplyToAuthorEnabled { get; set; }

        [DataMember(Name = "suggested_users")]
        public SuggestedUsers SuggestedUsers { get; set; }

        [DataMember(Name = "is_new_suggestion")]
        public bool? IsNewSuggestion { get; set; }

        [DataMember(Name = "comment_likes_enabled")]
        public bool? CommentLikesEnabled { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "lat")]
        public double? Lat { get; set; }

        [DataMember(Name = "lng")]
        public double? Lng { get; set; }

        [DataMember(Name = "channel")]
        public Channel Channel { get; set; }

        [DataMember(Name = "gating")]
        public Gating Gating { get; set; }

        [DataMember(Name = "injected")]
        public Injected Injected { get; set; }

        [DataMember(Name = "placeholder")]
        public Placeholder Placeholder { get; set; }

        [DataMember(Name = "algorithm")]
        public string Algorithm { get; set; }

        [DataMember(Name = "connection_id")]
        public string ConnectionId { get; set; }

        [DataMember(Name = "social_context")]
        public string SocialContext { get; set; }

        [DataMember(Name = "icon")]
        public dynamic Icon { get; set; }

        [DataMember(Name = "media_ids")]
        public string[] MediaIds { get; set; }

        [DataMember(Name = "media_id")]
        public string MediaId { get; set; }

        [DataMember(Name = "thumbnail_urls")]
        public dynamic ThumbnailUrls { get; set; }

        [DataMember(Name = "large_urls")]
        public dynamic LargeUrls { get; set; }

        [DataMember(Name = "media_infos")]
        public dynamic MediaInfos { get; set; }

        [DataMember(Name = "value")]
        public double? Value { get; set; }

        [DataMember(Name = "collapse_comments")]
        public bool? CollapseComments { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "link_text")]
        public string LinkText { get; set; }

        [DataMember(Name = "link_hint_text")]
        public string LinkHintText { get; set; }

        [DataMember(Name = "iTunesItem")]
        public dynamic ItunesItem { get; set; }

        [DataMember(Name = "ad_header_style")]
        public int? AdHeaderStyle { get; set; }

        [DataMember(Name = "ad_metadata")]
        public AdMetadata[] AdMetadata { get; set; }

        [DataMember(Name = "ad_action")]
        public string AdAction { get; set; }

        [DataMember(Name = "ad_link_type")]
        public int? AdLinkType { get; set; }

        [DataMember(Name = "dr_ad_type")]
        public int? DrAdType { get; set; }

        [DataMember(Name = "android_links")]
        public AndroidLinks[] AndroidLinks { get; set; }

        [DataMember(Name = "ios_links")]
        public IosLinks[] IosLinks { get; set; }

        [DataMember(Name = "iab_autofill_optout_info")]
        public IabAutofillOptoutInfo IabAutofillOptoutInfo { get; set; }

        [DataMember(Name = "force_overlay")]
        public bool? ForceOverlay { get; set; }

        [DataMember(Name = "hide_nux_text")]
        public bool? HideNuxText { get; set; }

        [DataMember(Name = "overlay_text")]
        public string OverlayText { get; set; }

        [DataMember(Name = "overlay_title")]
        public string OverlayTitle { get; set; }

        [DataMember(Name = "overlay_subtitle")]
        public string OverlaySubtitle { get; set; }

        [DataMember(Name = "fb_page_url")]
        public Uri FbPageUrl { get; set; }

        [DataMember(Name = "playback_duration_secs")]
        public dynamic PlaybackDurationSecs { get; set; }

        [DataMember(Name = "url_expire_at_secs")]
        public dynamic UrlExpireAtSecs { get; set; }

        [DataMember(Name = "is_sidecar_child")]
        public dynamic IsSidecarChild { get; set; }

        [DataMember(Name = "comment_threading_enabled")]
        public bool? CommentThreadingEnabled { get; set; }

        [DataMember(Name = "cover_media")]
        public CoverMedia CoverMedia { get; set; }

        [DataMember(Name = "saved_collection_ids")]
        public string[] SavedCollectionIds { get; set; }

        [DataMember(Name = "boosted_status")]
        public dynamic BoostedStatus { get; set; }

        [DataMember(Name = "boost_unavailable_reason")]
        public dynamic BoostUnavailableReason { get; set; }

        [DataMember(Name = "viewers")]
        public User[] Viewers { get; set; }

        [DataMember(Name = "viewer_cursor")]
        public dynamic ViewerCursor { get; set; }

        [DataMember(Name = "total_viewer_count")]
        public int? TotalViewerCount { get; set; }

        [DataMember(Name = "multi_author_reel_names")]
        public dynamic MultiAuthorReelNames { get; set; }

        [DataMember(Name = "screenshotter_user_ids")]
        public dynamic ScreenshotterUserIds { get; set; }

        [DataMember(Name = "reel_share")]
        public ReelShare ReelShare { get; set; }

        [DataMember(Name = "organic_post_id")]
        public ulong? OrganicPostId { get; set; }

        [DataMember(Name = "sponsor_tags")]
        public User[] SponsorTags { get; set; }

        [DataMember(Name = "story_poll_voter_infos")]
        public dynamic StoryPollVoterInfos { get; set; }

        [DataMember(Name = "imported_taken_at")]
        public dynamic ImportedTakenAt { get; set; }

        [DataMember(Name = "lead_gen_form_id")]
        public string LeadGenFormId { get; set; }

        [DataMember(Name = "ad_id")]
        public ulong? AdId { get; set; }

        [DataMember(Name = "actor_fbid")]
        public string ActorFbid { get; set; }

        [DataMember(Name = "is_ad4ad")]
        public dynamic IsAd4Ad { get; set; }

        [DataMember(Name = "commenting_disabled_for_viewer")]
        public dynamic CommentingDisabledForViewer { get; set; }

        [DataMember(Name = "is_seen")]
        public dynamic IsSeen { get; set; }

        [DataMember(Name = "story_events")]
        public dynamic StoryEvents { get; set; }

        [DataMember(Name = "story_hashtags")]
        public StoryHashtag[] StoryHashtags { get; set; }

        [DataMember(Name = "story_polls")]
        public dynamic StoryPolls { get; set; }

        [DataMember(Name = "story_feed_media")]
        public dynamic StoryFeedMedia { get; set; }

        [DataMember(Name = "story_sound_on")]
        public dynamic StorySoundOn { get; set; }

        [DataMember(Name = "creative_config")]
        public dynamic CreativeConfig { get; set; }

        [DataMember(Name = "story_locations")]
        public StoryLocation[] StoryLocations { get; set; }

        [DataMember(Name = "story_sliders")]
        public dynamic StorySliders { get; set; }

        [DataMember(Name = "story_friend_lists")]
        public dynamic StoryFriendLists { get; set; }

        [DataMember(Name = "story_product_items")]
        public dynamic StoryProductItems { get; set; }

        [DataMember(Name = "story_questions")]
        public StoryQuestion[] StoryQuestions { get; set; }

        [DataMember(Name = "story_question_responder_infos")]
        public StoryQuestionResponderInfo[] StoryQuestionResponderInfos { get; set; }

        [DataMember(Name = "story_countdowns")]
        public StoryCountdown[] StoryCountdowns { get; set; }

        [DataMember(Name = "story_music_stickers")]
        public dynamic StoryMusicStickers { get; set; }

        [DataMember(Name = "supports_reel_reactions")]
        public bool? SupportsReelReactions { get; set; }

        [DataMember(Name = "show_one_tap_fb_share_tooltip")]
        public bool? ShowOneTapFbShareTooltip { get; set; }

        [DataMember(Name = "has_shared_to_fb")]
        public int? HasSharedToFb { get; set; }

        [DataMember(Name = "main_feed_carousel_starting_media_id")]
        public string MainFeedCarouselStartingMediaId { get; set; }

        [DataMember(Name = "main_feed_carousel_has_unseen_cover_media")]
        public bool? MainFeedCarouselHasUnseenCoverMedia { get; set; }

        [DataMember(Name = "inventory_source")]
        public string InventorySource { get; set; }

        [DataMember(Name = "is_eof")]
        public bool? IsEof { get; set; }

        [DataMember(Name = "top_followers")]
        public string[] TopFollowers { get; set; }

        [DataMember(Name = "top_followers_count")]
        public int? TopFollowersCount { get; set; }

        [DataMember(Name = "story_is_saved_to_archive")]
        public bool? StoryIsSavedToArchive { get; set; }

        [DataMember(Name = "timezone_offset")]
        public int? TimezoneOffset { get; set; }

        [DataMember(Name = "xpost_deny_reason")]
        public string XpostDenyReason { get; set; }

        [DataMember(Name = "product_tags")]
        public ProductTags ProductTags { get; set; }

        [DataMember(Name = "inline_composer_display_condition")]
        public string InlineComposerDisplayCondition { get; set; }

        [DataMember(Name = "inline_composer_imp_trigger_time")]
        public int? InlineComposerImpTriggerTime { get; set; }

        [DataMember(Name = "highlight_reel_ids")]
        public string[] HighlightReelIds { get; set; }

        [DataMember(Name = "total_screenshot_count")]
        public int? TotalScreenshotCount { get; set; }

        [DataMember(Name = "dominant_color")]
        public string DominantColor { get; set; }

        #endregion
    }
}