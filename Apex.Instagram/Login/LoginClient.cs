using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Request;
using Apex.Instagram.Request.Exception;
using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Login
{
    internal class LoginClient : IDisposable
    {
        #region Fields

        private readonly Account _account;

        #endregion

        public void Dispose() { }

        public async Task<LoginResponse> Login(string username, string password)
        {
            if ( string.IsNullOrWhiteSpace(username) )
            {
                throw new LoginException("You must provided a username to log in.");
            }

            if ( string.IsNullOrWhiteSpace(password) )
            {
                throw new LoginException("You must provided a password to log in.");
            }

            if ( !LoginInfo.IsLoggedIn )
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
                        if ( s.TwoFactorRequired )
                        {
                            return s;
                        }
                    }

                    throw;
                }

                await UpdateLoginState(response);

                return response;
            }
        }

        private async Task UpdateLoginState(LoginResponse response)
        {
            if ( !response.IsOk() )
            {
                throw new LoginException("Invalid login response provided.");
            }

            _account.AccountInfo.AccountId = response.LoggedInUser.Pk;
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

        private async Task LoginFlow(bool justLoggedIn)
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

                //TODO CONTINUE
            }
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