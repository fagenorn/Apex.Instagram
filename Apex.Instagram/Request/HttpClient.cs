using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.Model.Internal;
using Apex.Instagram.Model.Request;
using Apex.Instagram.Response;

namespace Apex.Instagram.Request
{
    internal class HttpClient : IDisposable
    {
        public HttpClient(Account account, CookieContainer cookies = null, Proxy proxy = null)
        {
            _account = account;

            _handler = new HttpClientHandler
                       {
                           CookieContainer          = cookies ?? new CookieContainer(),
                           Proxy                    = proxy?.GetWebProxy(),
                           UseProxy                 = true, // Will use system default proxy if no proxy is set, needed for loopback
                           AllowAutoRedirect        = true,
                           MaxAutomaticRedirections = 8,
                           AutomaticDecompression   = DecompressionMethods.Deflate | DecompressionMethods.GZip
                       };

            _request = new System.Net.Http.HttpClient(_handler)
                       {
                           Timeout = Constants.Request.Instance.Timeout
                       };

            _lastCookieSave = new LastAction(TimeSpan.FromSeconds(15)); // Save new cookies only every 15 seconds. Reducing this will cause saves to occur more often at the cost of performance.
            _lock           = new SemaphoreSlim(1);
        }

        public void Dispose()
        {
            _request?.Dispose();
            _handler?.Dispose();
        }

        public async Task UpdateProxy(Proxy proxy)
        {
            _handler.Proxy = proxy.GetWebProxy();
            await _account.Storage.Proxy.SaveAsync(proxy);
        }

        /// <summary>
        ///     Makes a request using the open connection.
        ///     IMPORTANT: Dispose reponse.
        /// </summary>
        /// <param name="request">The request object. Will be disposed after request has been made.</param>
        /// <returns>Make sure to dispose <see cref="HttpResponseMessage" /> or a memory leak can occur.</returns>
        public async Task<ResponseInfo<T>> GetResponseAsync<T>(HttpRequestMessage request) where T : Response.JsonMap.Response
        {
            await _lock.WaitAsync();
            try
            {
                _account.Logger.Debug<Account>(request);
                var result = await _request.SendAsync(request);
                _account.Logger.Debug<Account>(result);
                return await ResponseInfo<T>.CreateAsync<T>(result);
            }
            finally
            {
                if ( _lastCookieSave.Passed )
                {
                    await _account.Storage.Cookie.SaveAsync(_handler.CookieContainer);
                    _lastCookieSave.Update();
                }

                request.Dispose();
                _lock.Release();
            }
        }

        public string GetProxy()
        {
            return _handler.Proxy == null
                       ? string.Empty
                       : _handler.Proxy.GetProxy(Constants.Request.Instance.BaseUrl)
                                 .AbsoluteUri;
        }

        public string GetCookie(string key)
        {
            var cookies = new CookieCollection();
            foreach ( var uri in Constants.Request.Instance.CookieUrl )
            {
                cookies.Add(_handler.CookieContainer.GetCookies(uri));
            }

            return cookies[key]
                ?.Value;
        }

        #region Fields

        private readonly Account _account;

        private readonly HttpClientHandler _handler;

        private readonly LastAction _lastCookieSave;

        private readonly System.Net.Http.HttpClient _request;

        private readonly SemaphoreSlim _lock;

        #endregion
    }
}