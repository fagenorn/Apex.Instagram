using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Apex.Instagram.API.Constants
{
    internal class Request
    {
        public ImmutableDictionary<string, string>[] SupportedCapabilities { get; }

        public ImmutableDictionary<string, string> PermanentHeaders { get; }

        public string[] SurfaceParams { get; } =
            {
                "4715",
                "5734"
            };

        public Uri BaseUrl { get; } = new Uri("https://i.instagram.com/");

        public Uri[] CookieUrl { get; } // Urls of which cookies should be saved

        public ImmutableDictionary<int, Uri> ApiUrl { get; }

        public TimeSpan Timeout { get; } = TimeSpan.FromSeconds(30);

        public int MaxRequestRetries { get; } = 5;

        public string HeaderConnectionType { get; } = "WIFI";

        public string HeaderAcceptEncoding { get; } = "gzip,deflate";

        public string HeaderAcceptLanguage { get; } = "en-US";

        public string HeaderFacebookAnalyticsApplicationId { get; } = "567067343352427";

        public bool HeaderXIgEuDcEnabled { get; } = true;

        public bool HeaderXIgVp9Capable { get; } = false;

        public string UserAgentLocale { get; } = "en_US";

        #region Singleton     

        private static Request _instance;

        private static readonly object Lock = new object();

        private Request()
        {
            SupportedCapabilities = new[]
                                    {
                                        ImmutableDictionary.CreateRange(new[]
                                                                        {
                                                                            new KeyValuePair<string, string>("name", "SUPPORTED_SDK_VERSIONS"),
                                                                            new KeyValuePair<string, string>("value", "13.0,14.0,15.0,16.0,17.0,18.0,19.0,20.0,21.0,22.0,23.0,24.0,25.0,26.0,27.0,28.0,29.0,30.0,31.0,32.0,33.0,34.0,35.0,36.0,37.0,38.0,39.0,40.0,41.0,42.0,43.0,44.0,45.0,46.0,47.0,48.0,49.0,50.0,51.0,52.0,53.0,54.0,55.0,56.0,57.0,58.0,59.0,60.0,61.0,62.0,63.0")
                                                                        }),
                                        ImmutableDictionary.CreateRange(new[]
                                                                        {
                                                                            new KeyValuePair<string, string>("name", "FACE_TRACKER_VERSION"),
                                                                            new KeyValuePair<string, string>("value", "12")
                                                                        }),
                                        ImmutableDictionary.CreateRange(new[]
                                                                        {
                                                                            new KeyValuePair<string, string>("name", "segmentation"),
                                                                            new KeyValuePair<string, string>("value", "segmentation_enabled")
                                                                        }),
                                        ImmutableDictionary.CreateRange(new[]
                                                                        {
                                                                            new KeyValuePair<string, string>("name", "COMPRESSION"),
                                                                            new KeyValuePair<string, string>("value", "ETC2_COMPRESSION")
                                                                        }),
                                        ImmutableDictionary.CreateRange(new[]
                                                                        {
                                                                            new KeyValuePair<string, string>("name", "world_tracker"),
                                                                            new KeyValuePair<string, string>("value", "world_tracker_enabled")
                                                                        }),
                                        ImmutableDictionary.CreateRange(new[]
                                                                        {
                                                                            new KeyValuePair<string, string>("name", "gyroscope"),
                                                                            new KeyValuePair<string, string>("value", "gyroscope_enabled")
                                                                        })
                                    };

            ApiUrl = ImmutableDictionary.CreateRange(new[]
                                                     {
                                                         new KeyValuePair<int, Uri>(1, new Uri(BaseUrl, "api/v1/")),
                                                         new KeyValuePair<int, Uri>(2, new Uri(BaseUrl, "api/v2/"))
                                                     });

            CookieUrl = new[]
                        {
                            new Uri("https://httpbin.org"),
                            new Uri("http://ptsv2.com"),
                            BaseUrl
                        };

            PermanentHeaders = ImmutableDictionary.CreateRange(new[]
                                                               {
                                                                   new KeyValuePair<string, string>("Accept", "*/*"),
                                                                   new KeyValuePair<string, string>("Accept-Encoding", HeaderAcceptEncoding),
                                                                   new KeyValuePair<string, string>("Accept-Language", HeaderAcceptLanguage)
                                                               });
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