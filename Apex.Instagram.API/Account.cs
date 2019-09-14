using System;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.API.Logger;
using Apex.Instagram.API.Login;
using Apex.Instagram.API.Login.Challenge;
using Apex.Instagram.API.Model.Account;
using Apex.Instagram.API.Model.Account.Device;
using Apex.Instagram.API.Request;
using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Request.Instagram;
using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Storage;
using Apex.Shared.Model;

using JetBrains.Annotations;

namespace Apex.Instagram.API
{
    /// <summary>
    ///     The Instagram account.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public sealed class Account : IDisposable
    {
        #region Fields

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        #endregion

        internal string GetCookie([NotNull] string key) { return HttpClient.GetCookie(key); }

        internal async Task<T> ApiRequest<T>(RequestBuilder builder) where T : Response.JsonMap.Response
        {
            if ( _cancellationTokenSource.IsCancellationRequested )
            {
                throw new ObjectDisposedException(nameof(Account));
            }

            using var result = await HttpClient.GetResponseAsync<T>(builder, _cancellationTokenSource.Token)
                                               .ConfigureAwait(false);

            return result.Response;
        }

        #region Public Methods

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            HttpClient?.Dispose();
            LoginClient?.Dispose();
            Storage?.Dispose();
        }

        /// <summary>
        ///     Updates the proxy.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        public async Task UpdateProxyAsync(Proxy proxy)
        {
            await HttpClient.UpdateProxy(proxy)
                            .ConfigureAwait(false);
        }

        /// <summary>
        ///     Updates the username.
        /// </summary>
        /// <param name="username">The username.</param>
        public async Task UpdateUsernameAsync(string username)
        {
            AccountInfo.Username = username;
            await Storage.AccountInfo.SaveAsync(AccountInfo)
                         .ConfigureAwait(false);
        }

        /// <summary>
        ///     Updates the password.
        /// </summary>
        /// <param name="password">The password.</param>
        public async Task UpdatePasswordAsync(string password)
        {
            AccountInfo.Password = password;
            await Storage.AccountInfo.SaveAsync(AccountInfo)
                         .ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets the proxy as an absolute uri. An empty string will be returned if no proxy is set.
        /// </summary>
        /// <returns>The proxy string</returns>
        public string GetProxy() { return HttpClient.GetProxy(); }

        /// <summary>Attempt to log into the account.</summary>
        /// <returns>
        ///     <see cref="LoginResponse" />
        /// </returns>
        public async Task<LoginResponse> LoginAsync()
        {
            return await LoginClient.Login()
                                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets the challenge client.
        ///     Should be called after <see cref="ChallengeClient" />
        /// </summary>
        /// <returns>
        ///     <see cref="ChallengeRequiredException" />
        /// </returns>
        public async Task<ChallengeClient> GetChallengeClientAsync()
        {
            return await LoginClient.ChallengeLogin()
                                    .ConfigureAwait(false);
        }

        #endregion

        #region Request Collections

        internal Internal Internal { get; }

        internal Profile Profile { get; }

        internal Timeline Timeline { get; }

        internal Story Story { get; }

        internal Discover Discover { get; }

        internal Direct Direct { get; }

        /// <inheritdoc cref="People" />
        public People People { get; }

        internal Media Media { get; }

        internal Tv Tv { get; }

        internal Creative Creative { get; }

        #endregion

        #region Properties

        public AccountInfo AccountInfo { get; private set; }

        internal LoginClient LoginClient { get; private set; }

        internal HttpClient HttpClient { get; private set; }

        internal StorageManager Storage { get; }

        internal IApexLogger Logger { get; }

        #endregion

        #region Constructor

        private Account(IStorage storage, int id, IApexLogger logger = null)
        {
            Initialization.Initialize();

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
            Tv       = new Tv(this);
            Creative = new Creative(this);
        }

        private async Task<Account> InitializeAsync()
        {
            AccountInfo = await Storage.AccountInfo.LoadAsync()
                                       .ConfigureAwait(false);

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

                await Storage.AccountInfo.SaveAsync(AccountInfo)
                             .ConfigureAwait(false);
            }

            LoginClient = await LoginClient.CreateAsync(this)
                                           .ConfigureAwait(false); // This needs to be created first so middleware can be initialized correctly.

            HttpClient = await HttpClient.CreateAsync(this)
                                         .ConfigureAwait(false);

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