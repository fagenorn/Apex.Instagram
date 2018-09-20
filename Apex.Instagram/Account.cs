using System;
using System.Net.Http;
using System.Threading.Tasks;

using Apex.Instagram.Logger;
using Apex.Instagram.Login;
using Apex.Instagram.Model.Account;
using Apex.Instagram.Model.Account.Device;
using Apex.Instagram.Model.Request;
using Apex.Instagram.Request.Instagram;
using Apex.Instagram.Storage;

using HttpClient = Apex.Instagram.Request.HttpClient;

namespace Apex.Instagram
{
    public class Account : IDisposable
    {
        public void Dispose()
        {
            HttpClient?.Dispose();
            LoginClient?.Dispose();
            Storage?.Dispose();
        }

        public async Task UpdateProxy(Proxy proxy) { await HttpClient.UpdateProxy(proxy); }

        public async Task UpdateUsername(string username)
        {
            AccountInfo.Username = username;
            await Storage.AccountInfo.SaveAsync(AccountInfo);
        }

        public async Task UpdatePassword(string password)
        {
            AccountInfo.Password = password;
            await Storage.AccountInfo.SaveAsync(AccountInfo);
        }

        public string GetProxy() { return HttpClient.GetProxy(); }

        internal string GetCookie(string key) { return HttpClient.GetCookie(key); }

        internal async Task<T> ApiRequest<T>(HttpRequestMessage request) where T : Response.JsonMap.Response
        {
            using (var result = await HttpClient.GetResponseAsync<T>(request))
            {
                return result.Response;
            }
        }

        public async Task Login() { await LoginClient.Login(); }

        #region Request Collections

        internal Internal Internal { get; }

        internal Profile Profile { get; }

        internal Timeline Timeline { get; }

        internal Story Story { get; }

        internal Discover Discover { get; }

        internal Direct Direct { get; }

        internal People People { get; }

        internal Media Media { get; }

        #endregion

        #region Properties

        internal LoginClient LoginClient { get; private set; }

        internal AccountInfo AccountInfo { get; private set; }

        internal HttpClient HttpClient { get; private set; }

        internal StorageManager Storage { get; }

        internal IApexLogger Logger { get; }

        #endregion

        #region Constructor

        private Account(StorageManager storage, IApexLogger logger = null)
        {
            Storage = storage;
            Logger  = logger ?? new NullLogger();

            Internal = new Internal(this);
            Profile  = new Profile(this);
            Timeline = new Timeline(this);
            Story    = new Story(this);
            Discover = new Discover(this);
            Direct   = new Direct(this);
            People   = new People(this);
            Media    = new Media(this);
        }

        private async Task<Account> InitializeAsync()
        {
            AccountInfo = await Storage.AccountInfo.LoadAsync();
            if ( AccountInfo == null )
            {
                AccountInfo = new AccountInfo
                              {
                                  DeviceId      = Utils.Instagram.Instance.GenerateDeviceId(),
                                  PhoneId       = Utils.Instagram.Instance.GenerateUuid(),
                                  Uuid          = Utils.Instagram.Instance.GenerateUuid(),
                                  AdvertisingId = Utils.Instagram.Instance.GenerateUuid(),
                                  SessionId     = Utils.Instagram.Instance.GenerateUuid(),
                                  DeviceInfo    = DeviceGenerator.Instance.Get()
                              };

                await Storage.AccountInfo.SaveAsync(AccountInfo);
            }

            LoginClient = await LoginClient.CreateAsync(this); // This needs to be created first so middlewares can be initialized correctly.
            HttpClient  = await HttpClient.CreateAsync(this);

            return this;
        }

        internal static Task<Account> CreateAsync(StorageManager storage, IApexLogger logger = null)
        {
            var ret = new Account(storage, logger);

            return ret.InitializeAsync();
        }

        #endregion
    }
}