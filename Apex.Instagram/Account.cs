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
        #region Request Collections

        internal Internal Internal { get; }

        #endregion

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

        public async Task Login(string username, string password) { await LoginClient.Login(AccountInfo.Username, AccountInfo.Password); }

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
        }

        private async Task<Account> InitializeAsync()
        {
            AccountInfo = await Storage.AccountInfo.LoadAsync();
            HttpClient  = await HttpClient.CreateAsync(this);
            LoginClient = await LoginClient.CreateAsync(this);

            if ( AccountInfo == null )
            {
                // ToDo: New account, generate needed information.
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