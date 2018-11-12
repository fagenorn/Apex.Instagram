﻿using System;
using System.Net.Http;
using System.Threading;
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
        #region Fields

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        #endregion

        internal string GetCookie(string key) { return HttpClient.GetCookie(key); }

        internal async Task<T> ApiRequest<T>(Func<HttpRequestMessage> request) where T : Response.JsonMap.Response
        {
            if ( _cancellationTokenSource.IsCancellationRequested )
            {
                throw new ObjectDisposedException(nameof(Account));
            }

            using (var result = await HttpClient.GetResponseAsync<T>(request, _cancellationTokenSource.Token).ConfigureAwait(false))
            {
                return result.Response;
            }
        }

        #region Public Methods

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            HttpClient?.Dispose();
            LoginClient?.Dispose();
            Storage?.Dispose();
        }

        public async Task UpdateProxy(Proxy proxy) { await HttpClient.UpdateProxy(proxy).ConfigureAwait(false); }

        public async Task UpdateUsername(string username)
        {
            AccountInfo.Username = username;
            await Storage.AccountInfo.SaveAsync(AccountInfo).ConfigureAwait(false);
        }

        public async Task UpdatePassword(string password)
        {
            AccountInfo.Password = password;
            await Storage.AccountInfo.SaveAsync(AccountInfo).ConfigureAwait(false);
        }

        public string GetProxy() { return HttpClient.GetProxy(); }

        /// <summary>
        ///     Log into the account. A full login flow will only be done if needed.
        /// </summary>
        public async Task Login() { await LoginClient.Login().ConfigureAwait(false); }

        #endregion

        #region Request Collections

        internal Internal Internal { get; }

        internal Profile Profile { get; }

        internal Timeline Timeline { get; }

        internal Story Story { get; }

        internal Discover Discover { get; }

        internal Direct Direct { get; }

        public People People { get; }

        internal Media Media { get; }

        #endregion

        #region Properties

        internal LoginClient LoginClient { get; private set; }

        internal AccountInfo AccountInfo { get; private set; }

        internal HttpClient HttpClient { get; private set; }

        internal StorageManager Storage { get; }

        internal IApexLogger Logger { get; }

        internal int Id { get; }

        #endregion

        #region Constructor

        private Account(IStorage storage, int id, IApexLogger logger = null)
        {
            Id      = id;
            Storage = new StorageManager(storage, id, _cancellationTokenSource.Token);
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
            AccountInfo = await Storage.AccountInfo.LoadAsync().ConfigureAwait(false);
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

                await Storage.AccountInfo.SaveAsync(AccountInfo).ConfigureAwait(false);
            }

            LoginClient = await LoginClient.CreateAsync(this).ConfigureAwait(false); // This needs to be created first so middlewares can be initialized correctly.
            HttpClient  = await HttpClient.CreateAsync(this).ConfigureAwait(false);

            return this;
        }

        internal static Task<Account> CreateAsync(IStorage storage, int id, IApexLogger logger = null)
        {
            var ret = new Account(storage, id, logger);

            return ret.InitializeAsync();
        }

        #endregion
    }
}