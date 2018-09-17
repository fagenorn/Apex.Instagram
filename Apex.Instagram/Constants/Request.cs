using System;
using System.Collections.Generic;

namespace Apex.Instagram.Constants
{
    internal class Request
    {
        public Dictionary<string, string>[] SupportedCapabilities { get; } =
            {
                new Dictionary<string, string>
                {
                    {
                        "name", "SUPPORTED_SDK_VERSIONS"
                    },
                    {
                        "value", "9.0,10.0,11.0,12.0,13.0,14.0,15.0,16.0,17.0,18.0,19.0,20.0,21.0,22.0,23.0,24.0,25.0,26.0,27.0,28.0,29.0,30.0,31.0,32.0,33.0,34.0,35.0,36.0,37.0,38.0,39.0,40.0,41.0,42.0,43.0"
                    }
                },
                new Dictionary<string, string>
                {
                    {
                        "name", "FACE_TRACKER_VERSION"
                    },
                    {
                        "value", "10"
                    }
                },
                new Dictionary<string, string>
                {
                    {
                        "name", "segmentation"
                    },
                    {
                        "value", "segmentation_enabled"
                    }
                },
                new Dictionary<string, string>
                {
                    {
                        "name", "WORLD_TRACKER"
                    },
                    {
                        "value", "WORLD_TRACKER_ENABLED"
                    }
                }
            };

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