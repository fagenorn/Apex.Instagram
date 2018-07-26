using System;
using System.Collections.Generic;

namespace Apex.Instagram.Constants
{
    internal class Request
    {
        public Uri BaseUrl { get; } = new Uri("https://i.instagram.com/");

        public Uri[] CookieUrl { get; } // Urls of which cookies should be saved

        public Dictionary<int, string> ApiUrl { get; }

        public TimeSpan Timeout { get; } = TimeSpan.FromSeconds(30);

        public string HeaderConnectionType { get; } = "WIFI";

        public string HeaderCapabilities { get; } = "3brTPw==";

        public string HeaderFacebookAnalyticsApplicationId { get; } = "567067343352427";

        #region Singleton     

        private static Request _instance;

        private static readonly object Lock = new object();

        private Request()
        {
            ApiUrl = new Dictionary<int, string>
                     {
                         {
                             1, $"{BaseUrl}api/v1/"
                         },
                         {
                             2, $"{BaseUrl}api/v2/"
                         }
                     };

            CookieUrl = new[]
                        {
                            new Uri("https://httpbin.org"),
                            new Uri("http://ptsv2.com"),
                            BaseUrl
                        };
        }

        public static Request Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Request());
                }
            }
        }

        #endregion
    }
}