using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Apex.Instagram.API.Login.Challenge;
using Apex.Instagram.API.Login.Exception;
using Apex.Instagram.API.Request;
using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Utils;
using Apex.Shared.Model;

namespace Apex.Instagram.API.Login
{
    internal class LoginClient : IDisposable
    {
        #region Fields

        private readonly Account _account;

        #endregion

        public void Dispose() { }

        public async Task<LoginResponse> Login()
        {
            if ( string.IsNullOrWhiteSpace(_account.AccountInfo.Username) )
            {
                throw new LoginException("Username can't be empty.");
            }

            if ( string.IsNullOrWhiteSpace(_account.AccountInfo.Password) )
            {
                throw new LoginException("Password can't be empty.");
            }

            try
            {
                return await InternalLogin()
                           .ConfigureAwait(false);
            }
            catch (ChallengeRequiredException e)
            {
                LoginInfo.HasChallenge = true;

                await _account.Storage.ChallengeInfo.SaveAsync(e.Challenge)
                              .ConfigureAwait(false);

                await _account.Storage.LoginInfo.SaveAsync(LoginInfo)
                              .ConfigureAwait(false);

                throw;
            }
        }

        public async Task<ChallengeClient> ChallengeLogin()
        {
            var client = await ChallengeClient.CreateAsync(_account);

            if ( !client.HasChallenge )
            {
                throw new ChallengeException("No challenge available.");
            }

            if ( client.ChallengeInfo.LogOut )
            {
                // ToDo: log out
            }

            return client;
        }

        private async Task<LoginResponse> InternalLogin(bool forceLogin = false)
        {
            if ( LoginInfo.IsLoggedIn && !forceLogin )
            {
                return await LoginFlow(false)
                           .ConfigureAwait(false);
            }

            await PreLoginFlow()
                .ConfigureAwait(false);

            LoginResponse response;

            try
            {
                var request = new RequestBuilder(_account).SetNeedsAuth(false)
                                                          .SetUrl("accounts/login/")
                                                          .AddPost("country_codes", "[{\"country_code\":\"1\",\"source\":[\"default\",\"sim\"]}]")
                                                          .AddPost("phone_id", _account.AccountInfo.PhoneId)
                                                          .AddPost("_csrftoken", CsrfToken)
                                                          .AddPost("username", _account.AccountInfo.Username)
                                                          .AddPost("adid", _account.AccountInfo.AdvertisingId)
                                                          .AddPost("guid", _account.AccountInfo.Uuid)
                                                          .AddPost("device_id", _account.AccountInfo.DeviceId)
                                                          .AddPost("password", _account.AccountInfo.Password)
                                                          .AddPost("google_tokens", "[]")
                                                          .AddPost("login_attempt_count", 0);

                response = await _account.ApiRequest<LoginResponse>(request)
                                         .ConfigureAwait(false);
            }
            catch (RequestException e)
            {
                if ( e.HasResponse && e.Response is LoginResponse s )
                {
                    if ( s.TwoFactorRequired is bool tf && tf )
                    {
                        return s;
                    }
                }

                throw;
            }

            await UpdateLoginState(response)
                .ConfigureAwait(false);

            await LoginFlow(true)
                .ConfigureAwait(false);

            return response;
        }

        internal async Task UpdateLoginState(LoginResponse response)
        {
            if ( !response.IsOk() )
            {
                throw new LoginException("Invalid login response provided.");
            }

            Debug.Assert(response.LoggedInUser.Pk != null, "response.LoggedInUser.Pk != null");
            _account.AccountInfo.AccountId = response.LoggedInUser.Pk.Value;
            await _account.Storage.AccountInfo.SaveAsync(_account.AccountInfo)
                          .ConfigureAwait(false);

            LoginInfo.IsLoggedIn = true;
            LoginInfo.LastLogin.Update();
            await _account.Storage.LoginInfo.SaveAsync(LoginInfo)
                          .ConfigureAwait(false);
        }

        private async Task PreLoginFlow()
        {
            _account.HttpClient.ZeroRatingMiddleware.Reset();

            _account.HttpClient.StartEmulatingBatch();

            try
            {
                await _account.Internal.ReadMsisdnHeader("default")
                              .ConfigureAwait(false);

                await _account.Internal.BootstrapMsisdnHeader()
                              .ConfigureAwait(false);

                await _account.Internal.SyncDeviceFeatures(true)
                              .ConfigureAwait(false);

                await _account.Internal.SendLauncherSync(true)
                              .ConfigureAwait(false);

                await _account.Profile.GetPrefillCandidates()
                              .ConfigureAwait(false);

                await _account.Internal.LogAttribution()
                              .ConfigureAwait(false);

                await _account.Internal.FetchZeroRatingToken()
                              .ConfigureAwait(false);
            }
            finally
            {
                _account.HttpClient.StopEmulatingBatch();
            }

            await _account.Profile.SetContactPointPrefill("prefill")
                          .ConfigureAwait(false);
        }

        internal async Task<LoginResponse> LoginFlow(bool justLoggedIn)
        {
            if ( justLoggedIn )
            {
                _account.HttpClient.ZeroRatingMiddleware.Reset();

                _account.HttpClient.StartEmulatingBatch();

                try
                {
                    await _account.Profile.GetAccountFamily()
                                  .ConfigureAwait(false);

                    await _account.Internal.SendLauncherSync(false)
                                  .ConfigureAwait(false);

                    await _account.Internal.SyncUserFeatures()
                                  .ConfigureAwait(false);
                }
                finally
                {
                    _account.HttpClient.StopEmulatingBatch();
                }

                await _account.Story.GetReelsTrayFeed()
                              .ConfigureAwait(false);

                await _account.Tv.GetTvGuide()
                              .ConfigureAwait(false);

                await _account.Timeline.GetTimelineFeed(null, new Dictionary<string, object>
                                                              {
                                                                  {"recovered_from_crash", true}
                                                              })
                              .ConfigureAwait(false);

                await _account.Discover.GetSuggestedSearches("users")
                              .ConfigureAwait(false);

                await _account.Discover.GetRecentSearches()
                              .ConfigureAwait(false);

                await _account.Discover.GetSuggestedSearches("blended")
                              .ConfigureAwait(false);

                await _account.Internal.FetchZeroRatingToken()
                              .ConfigureAwait(false);

                await _account.Direct.GetRankedRecipients("reshare", true)
                              .ConfigureAwait(false);

                await _account.Direct.GetRankedRecipients("raven", true)
                              .ConfigureAwait(false);

                await _account.Direct.GetInbox()
                              .ConfigureAwait(false);

                await _account.Internal.LogResurrectAttribution()
                              .ConfigureAwait(false);

                await _account.Direct.GetPresences()
                              .ConfigureAwait(false);

                await _account.People.GetRecentActivityInbox()
                              .ConfigureAwait(false);

                var cpuExp = int.TryParse(LoginInfo.GetExperimentParam("ig_android_loom_universe", "cpu_sampling_rate_ms", "0"), out var temp) ? temp : 0;
                if ( cpuExp > 0 )
                {
                    await _account.Internal.GetLoomFetchConfig()
                                  .ConfigureAwait(false);
                }

                await _account.People.GetBootstrapUsers()
                              .ConfigureAwait(false);

                await _account.Profile.GetFacebookId()
                              .ConfigureAwait(false);

                await _account.Internal.GetDeviceCapabilitiesDecisions()
                              .ConfigureAwait(false);

                await _account.Media.GetBlockedMedia()
                              .ConfigureAwait(false);

                await _account.Creative.SendSupportedCapabilities()
                              .ConfigureAwait(false);

                await _account.Profile.GetLinkageStatus()
                              .ConfigureAwait(false);

                await _account.Internal.GetQpCooldowns()
                              .ConfigureAwait(false);

                await _account.Internal.GetArlinkDownloadInfo()
                              .ConfigureAwait(false);

                await _account.Discover.GetExploreFeed(null, true)
                              .ConfigureAwait(false);

                await _account.Profile.GetProcessContactPointSignals()
                              .ConfigureAwait(false);

                await _account.Internal.GetQpFetch()
                              .ConfigureAwait(false);

                await _account.People.GetSharePrefill()
                              .ConfigureAwait(false);

                await _account.Internal.GetFacebookOta()
                              .ConfigureAwait(false);
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
                    isPullToRefresh = Randomizer.Instance.Number(3) < 2;
                }

                _account.HttpClient.StartEmulatingBatch();

                try
                {
                    try
                    {
                        await _account.Timeline.GetTimelineFeed(null, new Dictionary<string, object>
                                                                      {
                                                                          {"is_pull_to_refresh", isPullToRefresh}
                                                                      })
                                      .ConfigureAwait(false);
                    }
                    catch (LoginRequiredException)
                    {
                        return await InternalLogin(true)
                                   .ConfigureAwait(false);
                    }

                    await _account.Story.GetReelsTrayFeed()
                                  .ConfigureAwait(false);
                }
                finally
                {
                    _account.HttpClient.StopEmulatingBatch();
                }

                if ( sessionExpired )
                {
                    LoginInfo.LastLogin.Update();
                    await _account.Storage.LoginInfo.SaveAsync(LoginInfo)
                                  .ConfigureAwait(false);

                    _account.AccountInfo.SessionId = Utils.Instagram.Instance.GenerateUuid();
                    await _account.Storage.AccountInfo.SaveAsync(_account.AccountInfo)
                                  .ConfigureAwait(false);

                    await _account.Tv.GetTvGuide()
                                  .ConfigureAwait(false);

                    _account.HttpClient.StartEmulatingBatch();

                    try
                    {
                        await _account.Internal.GetLoomFetchConfig()
                                      .ConfigureAwait(false);

                        await _account.Direct.GetRankedRecipients("reshare", true)
                                      .ConfigureAwait(false);

                        await _account.Direct.GetRankedRecipients("raven", true)
                                      .ConfigureAwait(false);

                        await _account.People.GetSelfInfo()
                                      .ConfigureAwait(false);

                        await _account.Direct.GetInbox()
                                      .ConfigureAwait(false);

                        await _account.People.GetRecentActivityInbox()
                                      .ConfigureAwait(false);

                        await _account.Discover.GetExploreFeed()
                                      .ConfigureAwait(false);
                    }
                    finally
                    {
                        _account.HttpClient.StopEmulatingBatch();
                    }
                }

                if ( LoginInfo.LastExperiments.Passed )
                {
                    _account.HttpClient.StartEmulatingBatch();

                    try
                    {
                        await _account.Internal.SyncUserFeatures()
                                      .ConfigureAwait(false);

                        await _account.Internal.SyncDeviceFeatures()
                                      .ConfigureAwait(false);
                    }
                    finally
                    {
                        _account.HttpClient.StopEmulatingBatch();
                    }
                }

                var expired = Epoch.Current - LoginInfo.ZrExpires;
                if ( expired > 0 )
                {
                    _account.HttpClient.ZeroRatingMiddleware.Reset();
                    await _account.Internal.FetchZeroRatingToken(expired > 7200 ? "token_stale" : "token_expired")
                                  .ConfigureAwait(false);
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
            LoginInfo = await _account.Storage.LoginInfo.LoadAsync()
                                      .ConfigureAwait(false) ?? new LoginInfo();

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