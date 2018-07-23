using System;
using System.Net.Http;
using System.Threading.Tasks;

using Apex.Instagram.Logger;
using Apex.Instagram.Model.Request;
using Apex.Instagram.Request;
using Apex.Instagram.Storage;

using HttpClient = Apex.Instagram.Request.HttpClient;

namespace Apex.Instagram
{
    public class Account : IDisposable
    {
        #region Fields

        private HttpClient _httpClient;

        #endregion

        public void Dispose()
        {
            _httpClient?.Dispose();
            Storage.Dispose();
        }

        public async Task UpdateProxy(Proxy proxy) { await _httpClient.UpdateProxy(proxy); }

        public string GetProxy() { return _httpClient.GetProxy(); }

        internal string GetCookie(string key) { return _httpClient.GetCookie(key); }

        internal async Task<HttpResponseMessage> ApiRequest(HttpRequestMessage request)
        {
            // VERY IMPORTANT TO DISPOSE RESPONSE OR MEMORY LEAK WILL OCCUR
            Logger.Debug<Account>(request);
            var result = await _httpClient.GetResponseAsync(request);
            Logger.Debug<Account>(result);

            return result;
        }

        #region Properties

        internal StorageManager Storage { get; }

        internal IApexLogger Logger { get; }

        internal bool IsLoggedIn { get; set; }

        #endregion

        #region Constructor

        private Account(StorageManager storage, IApexLogger logger = null)
        {
            Storage = storage;
            Logger  = logger ?? new NullLogger();
        }

        private async Task<Account> InitializeAsync()
        {
            var cooks = await Storage.Cookie.LoadAsync();
            _httpClient = new HttpClientBuilder(this).SetProxy(await Storage.Proxy.LoadAsync())
                                                     .SetCookieStorage(cooks)
                                                     .Build();

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