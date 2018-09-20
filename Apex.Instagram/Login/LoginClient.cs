using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Model.Internal;
using Apex.Instagram.Request;
using Apex.Instagram.Request.Exception;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Utils;

namespace Apex.Instagram.Login
{
    internal class LoginClient : IDisposable
    {
        #region Fields

        private readonly Account _account;

        #endregion

        public void Dispose() { }

        public async Task<LoginResponse> Login() { return await InternalLogin(); }

        private async Task<LoginResponse> InternalLogin(bool forceLogin = false)
        {
            if ( !LoginInfo.IsLoggedIn || forceLogin )
            {
                await PreLoginFlow();

                LoginResponse response;

                try
                {
                    var request = new RequestBuilder(_account).SetNeedsAuth(false)
                                                              .SetUrl("accounts/login/")
                                                              .AddPost("phone_id", _account.AccountInfo.PhoneId)
                                                              .AddPost("_csrftoken", CsrfToken)
                                                              .AddPost("username", _account.AccountInfo.Username)
                                                              .AddPost("adid", _account.AccountInfo.AdvertisingId)
                                                              .AddPost("guid", _account.AccountInfo.Uuid)
                                                              .AddPost("device_id", _account.AccountInfo.DeviceId)
                                                              .AddPost("password", _account.AccountInfo.Password)
                                                              .AddPost("login_attempt_count", 0)
                                                              .Build();

                    response = await _account.ApiRequest<LoginResponse>(request);
                }
                catch (RequestException e)
                {
                    if ( e.HasResponse && e.Response is LoginResponse s )
                    {
                        if (s.TwoFactorRequired is bool tf && tf)
                        {
                            return s;
                        }
                    }

                    throw;
                }

                await UpdateLoginState(response);
                await LoginFlow(true);

                return response;
            }

            return await LoginFlow(false);
        }

        private async Task UpdateLoginState(LoginResponse response)
        {
            if ( !response.IsOk() )
            {
                throw new LoginException("Invalid login response provided.");
            }

            _account.AccountInfo.AccountId = response.LoggedInUser.Pk.ToString();
            await _account.Storage.AccountInfo.SaveAsync(_account.AccountInfo);

            LoginInfo.IsLoggedIn = true;
            LoginInfo.LastLogin.Update();
        }

        private async Task PreLoginFlow()
        {
            _account.HttpClient.ZeroRatingMiddleware.Reset();
            await _account.Internal.ReadMsisdnHeader("ig_select_app");
            await _account.Internal.SyncDeviceFeatures(true);
            await _account.Internal.SendLauncherSync(true);
            await _account.Internal.LogAttribution();
            await _account.Internal.FetchZeroRatingToken();
            await _account.Profile.SetContactPointPrefill("prefill");
        }

        private async Task<LoginResponse> LoginFlow(bool justLoggedIn)
        {
            if ( justLoggedIn )
            {
                _account.HttpClient.ZeroRatingMiddleware.Reset();
                await _account.Internal.SendLauncherSync(false);
                await _account.Internal.SyncUserFeatures();
                await _account.Timeline.GetTimelineFeed(null, new Dictionary<string, object>
                                                              {
                                                                  {
                                                                      "recovered_from_crash", true
                                                                  }
                                                              });

                await _account.Story.GetReelsTrayFeed();
                await _account.Discover.GetSuggestedSearches("users");
                await _account.Discover.GetRecentSearches();
                await _account.Discover.GetSuggestedSearches("blended");
                await _account.Internal.FetchZeroRatingToken();
                await _account.Direct.GetRankedRecipients("reshare", true);
                await _account.Direct.GetRankedRecipients("raven", true);
                await _account.Direct.GetInbox();
                await _account.Direct.GetPresences();
                await _account.People.GetRecentActivityInbox();

                var cpuExp = int.TryParse(LoginInfo.GetExperimentParam("ig_android_loom_universe", "cpu_sampling_rate_ms", "0"), out var temp) ? temp : 0;
                if ( cpuExp > 0 )
                {
                    await _account.Internal.GetLoomFetchConfig();
                }

                await _account.Internal.GetProfileNotice();
                await _account.Media.GetBlockedMedia();
                await _account.People.GetBootstrapUsers();
                await _account.Discover.GetExploreFeed(null, true);
                await _account.Internal.GetQpFetch();
                await _account.Internal.GetFacebookOta();
            }
            else
            {
                var    sessionExpired = LoginInfo.LastLogin.Passed;
                object isPullToRefresh;
                if ( sessionExpired )
                {
                    isPullToRefresh = null;
                }
                else
                {
                    isPullToRefresh = Randomizer.Instance.Number(2) < 2;
                }

                try
                {
                    await _account.Timeline.GetTimelineFeed(null, new Dictionary<string, object>
                                                                  {
                                                                      {
                                                                          "is_pull_to_refresh", isPullToRefresh
                                                                      }
                                                                  });
                }
                catch (LoginRequiredException)
                {
                    return await InternalLogin(true);
                }

                if ( sessionExpired )
                {
                    LoginInfo.LastLogin.Update();
                    await _account.Storage.LoginInfo.SaveAsync(LoginInfo);

                    _account.AccountInfo.SessionId = Utils.Instagram.Instance.GenerateUuid();
                    await _account.Storage.AccountInfo.SaveAsync(_account.AccountInfo);

                    await _account.People.GetBootstrapUsers();
                    await _account.Story.GetReelsTrayFeed();
                    await _account.Direct.GetRankedRecipients("reshare", true);
                    await _account.Direct.GetRankedRecipients("raven", true);
                    await _account.Direct.GetInbox();
                    await _account.Direct.GetPresences();
                    await _account.People.GetRecentActivityInbox();
                    await _account.Internal.GetProfileNotice();
                    await _account.Discover.GetExploreFeed();
                }

                if ( LoginInfo.LastExperiments.Passed )
                {
                    await _account.Internal.SyncUserFeatures();
                    await _account.Internal.SyncDeviceFeatures();
                }

                var expired = Epoch.Current - LoginInfo.ZrExpires;
                if ( expired > 0 )
                {
                    _account.HttpClient.ZeroRatingMiddleware.Reset();
                    await _account.Internal.FetchZeroRatingToken(expired > 7200 ? "token_stale" : "token_expired");
                }
            }

            return null;
        }

        #region Properties

        internal LoginInfo LoginInfo { get; set; }

        internal string CsrfToken => _account.GetCookie("csrftoken");

        #endregion

        #region Constructor

        private LoginClient(Account account) { _account = account; }

        private async Task<LoginClient> InitializeAsync()
        {
            LoginInfo = await _account.Storage.LoginInfo.LoadAsync() ?? new LoginInfo();

            return this;
        }

        internal static Task<LoginClient> CreateAsync(Account account)
        {
            var ret = new LoginClient(account);

            return ret.InitializeAsync();
        }

        #endregion
    }
}