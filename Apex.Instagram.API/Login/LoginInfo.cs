﻿using System.Collections.Generic;
using System.Linq;

using Apex.Instagram.API.Constants;
using Apex.Shared.Model;

using MessagePack;

namespace Apex.Instagram.API.Login
{
    [MessagePackObject]
    internal class LoginInfo
    {
        private Dictionary<string, Dictionary<string, string>> _experiments;

        private LastAction _lastExperiments = new LastAction(Delays.Instance.ExperimentsRefreshInterval);

        private LastAction _lastLogin = new LastAction(Delays.Instance.AppRefreshInterval);

        [Key(0)]
        public bool IsLoggedIn { get; set; }

        [Key(1)]
        public Dictionary<string, string> ZrRules { get; set; }

        [Key(2)]
        public string ZrToken { get; set; }

        [Key(3)]
        public int ZrExpires { get; set; }

        [Key(4)]
        public LastAction LastLogin
        {
            get => _lastLogin;
            set
            {
                _lastLogin       = value;
                _lastLogin.Limit = Delays.Instance.AppRefreshInterval;
            }
        }

        [Key(5)]
        public Dictionary<string, Dictionary<string, string>> Experiments
        {
            get => _experiments;
            set
            {
                if ( value == null )
                {
                    _experiments = null;

                    return;
                }

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
        public LastAction LastExperiments
        {
            get => _lastExperiments;
            set
            {
                _lastExperiments       = value;
                _lastExperiments.Limit = Delays.Instance.ExperimentsRefreshInterval;
            }
        }

        [Key(7)]
        public bool HasChallenge { get; set; }

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
                "ig_android_loom_universe",
                "ig_android_live_suggested_live_expansion",
                "ig_android_live_qa_broadcaster_v1_universe"
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