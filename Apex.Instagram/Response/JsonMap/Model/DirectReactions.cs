using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class DirectReactions
    {
        [DataMember(Name = "likes_count")]
        public int? LikesCount { get; set; }

        [DataMember(Name = "likes")]
        public DirectReaction[] Likes { get; set; }
    }
}