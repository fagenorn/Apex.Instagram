using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Comment
    {
        public const int Parent = 0;

        public const int Child = 2;

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "user_id")]
        public ulong? UserId { get; set; }

        /// <summary>
        ///     Unix timestamp (UTC) of when the comment was posted.
        /// </summary>
        [DataMember(Name = "created_at")]
        public ulong? CreatedAt { get; set; }

        [Obsolete("Do not use. Use 'CreatedAt' instead.")]
        [DataMember(Name = "created_at_utc")]
        public ulong? CreatedAtUtc { get; set; }

        [DataMember(Name = "bit_flags")]
        public int? BitFlags { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "media_id")]
        public ulong? MediaId { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "content_type")]
        public string ContentType { get; set; }

        /// <summary>
        ///     A number describing what type of comment this is. Should be compared
        ///     against the <see cref="Parent" /> and <see cref="Child" /> constants. All
        ///     replies are of type <see cref="Child" />, and all parents are of type <see cref="Parent" />.
        /// </summary>
        [DataMember(Name = "type")]
        public int? Type { get; set; }

        [DataMember(Name = "comment_like_count")]
        public int? CommentLikeCount { get; set; }

        [DataMember(Name = "has_liked_comment")]
        public bool? HasLikedComment { get; set; }

        [DataMember(Name = "has_translation")]
        public bool? HasTranslation { get; set; }

        [DataMember(Name = "did_report_as_spam")]
        public bool? DidReportAsSpam { get; set; }

        [DataMember(Name = "parent_comment_id")]
        public ulong? ParentCommentId { get; set; }

        [DataMember(Name = "child_comment_count")]
        public int? ChildCommentCount { get; set; }

        [DataMember(Name = "preview_child_comments")]
        public Comment[] PreviewChildComments { get; set; }

        [DataMember(Name = "other_preview_users")]
        public User[] OtherPreviewUsers { get; set; }

        [DataMember(Name = "inline_composer_display_condition")]
        public string InlineComposerDisplayCondition { get; set; }

        /// <summary>
        ///     When <see cref="HasMoreTailChildComments" /> is true, you can use the value
        ///     in <see cref="NextMaxChildCursor" /> as "max_id" parameter to load up to
        ///     <see cref="NumTailChildComments" /> older child-comments.
        /// </summary>
        [DataMember(Name = "has_more_tail_child_comments")]
        public bool? HasMoreTailChildComments { get; set; }

        [DataMember(Name = "next_max_child_cursor")]
        public string NextMaxChildCursor { get; set; }

        [DataMember(Name = "num_tail_child_comments")]
        public int? NumTailChildComments { get; set; }

        /// <summary>
        ///     When <see cref="HasMoreHeadChildComments" /> is true, you can use the value
        ///     in <see cref="NextMinChildCursor" /> as "min_id" parameter to load up to
        ///     <see cref="NumHeadChildComments" /> newer child-comments.
        /// </summary>
        [DataMember(Name = "has_more_head_child_comments")]
        public bool? HasMoreHeadChildComments { get; set; }

        [DataMember(Name = "next_min_child_cursor")]
        public string NextMinChildCursor { get; set; }

        [DataMember(Name = "num_head_child_comments")]
        public int? NumHeadChildComments { get; set; }
    }
}