using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Challenge
    {
        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "api_path")]
        public dynamic ApiPath { get; set; }

        [DataMember(Name = "hide_webview_header")]
        public dynamic HideWebviewHeader { get; set; }

        [DataMember(Name = "lock")]
        public dynamic Lock { get; set; }

        [DataMember(Name = "logout")]
        public dynamic Logout { get; set; }

        [DataMember(Name = "native_flow")]
        public dynamic NativeFlow { get; set; }
    }
}