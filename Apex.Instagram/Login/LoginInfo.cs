using System;
using System.Collections.Generic;
using System.Linq;

using Apex.Instagram.Constants;
using Apex.Instagram.Model.Internal;

using MessagePack;

namespace Apex.Instagram.Login
{
    [MessagePackObject]
    internal class LoginInfo
    {
        private Dictionary<string, Dictionary<string, string>> _experiments;

        [Key(0)]
        public bool IsLoggedIn { get; set; }

        [Key(1)]
        public Dictionary<string, string> ZrRules { get; set; }

        [Key(2)]
        public string ZrToken { get; set; }

        [Key(3)]
        public int ZrExpires { get; set; }

        [Key(4)]
        public LastAction LastLogin { get; set; } = new LastAction(Delays.Instance.AppRefreshInterval);

        [Key(5)]
        public Dictionary<string, Dictionary<string, string>> Experiments
        {
            get => _experiments;
            set
            {
                var filtered = new Dictionary<string, Dictionary<string, string>>();
                foreach ( var key in ExperimentKeys )
                {
                    if ( value.ContainsKey(key) )
                    {
                        filtered[key] = value[key];
                    }
                }

                _experiments = filtered;
            }
        }

        [Key(6)]
        public LastAction LastExperiments { get; set; } = new LastAction(TimeSpan.FromMinutes(120));

        private string[] ExperimentKeys { get; } =
            {
                "ig_android_2fac",
                "ig_android_realtime_iris",
                "ig_android_skywalker_live_event_start_end",
                "ig_android_gqls_typing_indicator",
                "ig_android_upload_reliability_universe",
                "ig_android_photo_fbupload_universe",
                "ig_android_video_segmented_upload_universe",
                "ig_android_direct_video_segmented_upload_universe",
                "ig_android_reel_raven_video_segmented_upload_universe",
                "ig_android_ad_async_ads_universe",
                "ig_android_direct_inbox_presence",
                "ig_android_direct_thread_presence",
                "ig_android_rtc_reshare",
                "ig_android_sidecar_photo_fbupload_universe",
                "ig_android_fbupload_sidecar_video_universe",
                "ig_android_skip_get_fbupload_photo_universe",
                "ig_android_skip_get_fbupload_universe",
                "ig_android_loom_universe"
            };

        public bool IsExperimentEnabled(string experiment, string param, bool @default = false)
        {
            string[] goodValues =
            {
                "enabled",
                "true",
                "1"
            };

            if ( Experiments.ContainsKey(experiment) && Experiments[experiment]
                     .ContainsKey(param) )
            {
                return goodValues.Contains(Experiments[experiment][param]);
            }

            return @default;
        }

        public string GetExperimentParam(string experiment, string param, string @default = null)
        {
            if ( Experiments.ContainsKey(experiment) && Experiments[experiment]
                     .ContainsKey(param) )
            {
                return Experiments[experiment][param];
            }

            return @default;
        }
    }
}