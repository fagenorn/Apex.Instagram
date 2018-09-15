using System;
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

        public async Task Login(string username, string password)
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
                try
                {
                    await PreLoginFlow();
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

                    var response = await _account.ApiRequest<GenericResponse>(request);
                }
                catch (RequestException)
                {
                    // ToDo Check if two factor, or just rethrow.
                }
            }
        }

        private async Task PreLoginFlow()
        {
            _account.HttpClient.ZeroRatingMiddleware.Reset();
            await _account.Internal.ReadMsisdnHeader(@"ig_select_app");
        }

        private async Task LoginFlow() { }

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