﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Response.Serializer;
using Apex.Instagram.API.Utils;

namespace Apex.Instagram.API.Request.Instagram
{
    internal class Timeline : RequestCollection
    {
        public Timeline(Account account) : base(account) { }

        public async Task<TimelineFeedResponse> GetTimelineFeed(string maxId = null, Dictionary<string, object> options = null)
        {
            var asyncAds                  = Account.LoginClient.LoginInfo.IsExperimentEnabled("ig_android_ad_async_ads_universe", "is_enabled");
            var asyncAdsInHeadloadEnabled = Account.LoginClient.LoginInfo.IsExperimentEnabled("ig_android_ad_async_ads_universe", "is_async_ads_in_headload_enabled");
            var asyncAdsDoubleRequest     = Account.LoginClient.LoginInfo.IsExperimentEnabled("ig_android_ad_async_ads_universe", "is_double_request_enabled");
            var asyncAdsRti               = Account.LoginClient.LoginInfo.IsExperimentEnabled("ig_android_ad_async_ads_universe", "is_rti_enabled");
            var rtiDeliveryBackend        = Account.LoginClient.LoginInfo.IsExperimentEnabled("ig_android_ad_async_ads_universe", "rti_delivery_backend");

            var request = new RequestBuilder(Account).SetUrl("feed/timeline/")
                                                     .SetSignedPost(false)
                                                     .SetIsBodyCompressed(true)
                                                     .AddHeader("X-Ads-Opt-Out", "0")
                                                     .AddHeader("X-Google-AD-ID", Account.AccountInfo.AdvertisingId)
                                                     .AddHeader("X-DEVICE-ID", Account.AccountInfo.Uuid)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("is_prefetch", "0")
                                                     .AddPost("phone_id", Account.AccountInfo.PhoneId)
                                                     .AddPost("device_id", Account.AccountInfo.Uuid)
                                                     .AddPost("client_session_id", Account.AccountInfo.SessionId)
                                                     .AddPost("battery_level", Randomizer.Instance.Number(100, 25))
                                                     .AddPost("is_charging", "0")
                                                     .AddPost("will_sound_on", "1")
                                                     .AddPost("is_on_screen", "true")
                                                     .AddPost("timezone_offset", Time.Instance.GetTimezoneOffset())
                                                     .AddPost("is_async_ads_in_headload_enabled", (asyncAds && asyncAdsInHeadloadEnabled ? 1 : 0).ToString())
                                                     .AddPost("is_async_ads_double_request", (asyncAds && asyncAdsDoubleRequest ? 1 : 0).ToString())
                                                     .AddPost("is_async_ads_rti", (asyncAds && asyncAdsRti ? 1 : 0).ToString())
                                                     .AddPost("rti_delivery_backend", (rtiDeliveryBackend ? 1 : 0).ToString());

            if ( ExistsOption("latest_story_pk") )
            {
                Debug.Assert(options != null, nameof(options) + " != null");
                request.AddPost("latest_story_pk", (string) options["latest_story_pk"]);
            }

            if ( maxId != null )
            {
                request.AddPost("reason", "pagination")
                       .AddPost("max_id", maxId)
                       .AddPost("is_pull_to_refresh", "0");
            }
            else if ( NonEmptyOption("is_pull_to_refresh") )
            {
                request.AddPost("reason", "pull_to_refresh")
                       .AddPost("is_pull_to_refresh", "1");
            }
            else if ( ExistsOption("is_pull_to_refresh") )
            {
                request.AddPost("reason", "warm_start_fetch")
                       .AddPost("is_pull_to_refresh", "0");
            }
            else
            {
                request.AddPost("reason", "cold_start_fetch")
                       .AddPost("is_pull_to_refresh", "0");
            }

            if ( ExistsOption("seen_posts") )
            {
                Debug.Assert(options != null, nameof(options) + " != null");
                var value = options["seen_posts"];
                if ( value is string[] array )
                {
                    request.AddPost("seen_posts", string.Join(",", array));
                }
                else
                {
                    request.AddPost("seen_posts", (string) value);
                }
            }
            else if ( maxId == null )
            {
                request.AddPost("seen_posts", string.Empty);
            }

            if ( ExistsOption("unseen_posts") )
            {
                Debug.Assert(options != null, nameof(options) + " != null");
                var value = options["unseen_posts"];
                if ( value is string[] array )
                {
                    request.AddPost("unseen_posts", string.Join(",", array));
                }
                else
                {
                    request.AddPost("unseen_posts", (string) value);
                }
            }
            else if ( maxId == null )
            {
                request.AddPost("unseen_posts", string.Empty);
            }

            if ( ExistsOption("feed_view_info") )
            {
                Debug.Assert(options != null, nameof(options) + " != null");
                var value = options["feed_view_info"];
                if ( value is string[] )
                {
                    request.AddPost("feed_view_info", JsonSerializer.Serialize(value, JsonSerializerDefaultOptions.Instance));
                }
                else
                {
                    var temp = new[]
                               {
                                   (string) value
                               };

                    request.AddPost("feed_view_info", JsonSerializer.Serialize(temp, JsonSerializerDefaultOptions.Instance));
                }
            }
            else if ( maxId == null )
            {
                request.AddPost("feed_view_info", string.Empty);
            }

            if ( NonEmptyOption("push_disabled") )
            {
                Debug.Assert(options != null, nameof(options) + " != null");
                request.AddPost("push_disabled", "true");
            }

            if ( NonEmptyOption("recovered_from_crash") )
            {
                Debug.Assert(options != null, nameof(options) + " != null");
                request.AddPost("recovered_from_crash", "1");
            }

            return await Account.ApiRequest<TimelineFeedResponse>(request)
                                .ConfigureAwait(false);

            bool ExistsOption(string name) { return options != null && options.ContainsKey(name); }

            bool NonEmptyOption(string name)
            {
                if ( options != null && options.ContainsKey(name) )
                {
                    var value = options[name]
                        ?.ToString();

                    return !string.IsNullOrWhiteSpace(value) && value != "false" && value != "0";
                }

                return false;
            }
        }
    }
}