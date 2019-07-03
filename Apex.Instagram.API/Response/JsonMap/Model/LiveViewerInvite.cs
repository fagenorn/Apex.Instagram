using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class LiveViewerInvite
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "broadcast")]
        public Broadcast Broadcast { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}