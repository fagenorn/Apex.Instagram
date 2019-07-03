using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Challenge
    {
        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "api_path")]
        public string ApiPath { get; set; }

        [DataMember(Name = "hide_webview_header")]
        public bool? HideWebviewHeader { get; set; }

        [DataMember(Name = "lock")]
        public bool? Lock { get; set; }

        [DataMember(Name = "logout")]
        public bool? Logout { get; set; }

        [DataMember(Name = "native_flow")]
        public bool? NativeFlow { get; set; }
    }
}