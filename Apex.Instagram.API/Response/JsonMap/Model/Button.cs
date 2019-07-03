using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Button
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "action")]
        public dynamic Action { get; set; }

        [DataMember(Name = "background_color")]
        public dynamic BackgroundColor { get; set; }

        [DataMember(Name = "border_color")]
        public dynamic BorderColor { get; set; }

        [DataMember(Name = "text_color")]
        public dynamic TextColor { get; set; }

        [DataMember(Name = "action_info")]
        public dynamic ActionInfo { get; set; }
    }
}