using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.Constants;
using Apex.Instagram.Model.Internal;
using Apex.Instagram.Model.Request;
using Apex.Instagram.Request.Exception;
using Apex.Instagram.Request.Middleware;
using Apex.Instagram.Response;

namespace Apex.Instagram.Request
{
    internal class HttpClient : IDisposable
    {
        public void Dispose()
        {
            _request?.Dispose();
            _handler?.Dispose();
        }

        public async Task UpdateProxy(Proxy proxy)
        {
            _proxy.Credentials = proxy == null
                                     ? null
                                     : proxy.HasCredentials
                                         ? proxy.Credentials
                                         : null;

            _proxy.Address = proxy?.Uri;
            await _account.Storage.Proxy.SaveAsync(proxy)
                          .ConfigureAwait(false);
        }

        /// <summary>
        ///     Makes a request using the open connection.
        ///     IMPORTANT: Dispose reponse.
        /// </summary>
        /// <param name="request">The request object. Will be disposed after request has been made.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Make sure to dispose <see cref="HttpResponseMessage" /> or a memory leak can occur.</returns>
        public async Task<ResponseInfo<T>> GetResponseAsync<T>(HttpRequestMessage request, CancellationToken ct = default) where T : Response.JsonMap.Response
        {
            await _lock.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                return await GetResponseAsyncInternal<T>(request, ct).ConfigureAwait(false);
            }
            catch (RequestException e)
            {
                _account.Logger.Error<HttpClient>(e, "An error occured while making a request.");

                throw;
            }
            finally
            {
                if ( !ct.IsCancellationRequested && _lastCookieSave.Passed )
                {
                    await _account.Storage.Cookie.SaveAsync(_handler.CookieContainer).ConfigureAwait(false);
                    _lastCookieSave.Update();
                }

                request.Dispose();
                _lock.Release();
            }
        }

        private async Task<ResponseInfo<T>> GetResponseAsyncInternal<T>(HttpRequestMessage request, CancellationToken ct, int depth = 1) where T : Response.JsonMap.Response
        {
            HttpResponseMessage result;
            _account.Logger.Debug<HttpClient>(request);

            try
            {
                result = await _request.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false);
                _account.Logger.Debug<HttpClient>(result);
            }
            catch (HttpRequestException e)
            {
                switch ( e.InnerException )
                {
                    case WebException webException when webException.Response is HttpWebResponse exceptionResponse:
                        result = new HttpResponseMessage(exceptionResponse.StatusCode);

                        break;
                    case IOException _ when depth < Constants.Request.Instance.MaxRequestRetries:

                        var requestClone = await CloneHttpRequestMessageAsync(request)
                                               .ConfigureAwait(false);

                        request.Dispose();

                        return await GetResponseAsyncInternal<T>(requestClone, ct, depth + 1)
                                   .ConfigureAwait(false);
                    default:

                        throw new RequestException("Critical request error occured.", e);
                }
            }
            catch (ObjectDisposedException e)
            {
                throw new RequestException("Request has been cancelled.", e);
            }
            catch (OperationCanceledException e)
            {
                throw new RequestException("Request has been cancelled.", e);
            }

            return await ResponseInfo<T>.CreateAsync<T>(result).ConfigureAwait(false);
        }

        public string GetProxy() { return _proxy.Address == null ? string.Empty : _proxy.Address.AbsoluteUri; }

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

        private async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
        {
            var clone = new HttpRequestMessage(request.Method, request.RequestUri);
            var ms    = new MemoryStream();
            if ( request.Content != null )
            {
                await request.Content.CopyToAsync(ms)
                             .ConfigureAwait(false);

                ms.Position   = 0;
                clone.Content = new StreamContent(ms);

                // Copy the content headers
                if ( request.Content.Headers != null )
                {
                    foreach ( var h in request.Content.Headers )
                    {
                        clone.Content.Headers.Add(h.Key, h.Value);
                    }
                }
            }

            clone.Version = request.Version;

            foreach ( var prop in request.Properties )
            {
                clone.Properties.Add(prop);
            }

            foreach ( var header in request.Headers )
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return clone;
        }

        #region Properties

        internal ZeroRatingMiddleware ZeroRatingMiddleware { get; }

        internal ModifyHeadersMiddleware ModifyHeadersMiddleware { get; }

        #endregion

        #region Constructor

        private HttpClient(Account account)
        {
            _account                = account;
            _lastCookieSave         = new LastAction(Delays.Instance.CookieSaveInterval);
            _lock                   = new SemaphoreSlim(1);
            ZeroRatingMiddleware    = new ZeroRatingMiddleware();
            ModifyHeadersMiddleware = new ModifyHeadersMiddleware(_account.AccountInfo.DeviceInfo.UserAgent);
        }

        private async Task<HttpClient> InitializeAsync()
        {
            var cookies = await _account.Storage.Cookie.LoadAsync()
                                        .ConfigureAwait(false);

            var proxy = await _account.Storage.Proxy.LoadAsync()
                                      .ConfigureAwait(false);

            _proxy = new WebProxy
                     {
                         Credentials = proxy == null
                                           ? null
                                           : proxy.HasCredentials
                                               ? proxy.Credentials
                                               : null,
                         Address = proxy?.Uri
                     };

            _handler = new HttpClientHandler
                       {
                           CookieContainer          = cookies ?? new CookieContainer(),
                           Proxy                    = _proxy,
                           UseProxy                 = true, // Will use system default proxy if no proxy is set, needed for loopback
                           AllowAutoRedirect        = true,
                           MaxAutomaticRedirections = 8,
                           AutomaticDecompression   = DecompressionMethods.Deflate | DecompressionMethods.GZip
                       };

            var middlewareHandler = new HttpClientMiddlewareHandler(_handler);
            _request = new System.Net.Http.HttpClient(middlewareHandler)
                       {
                           Timeout = Constants.Request.Instance.Timeout
                       };

            InjectMiddleware(middlewareHandler);

            return this;
        }

        private void InjectMiddleware(HttpClientMiddlewareHandler handler)
        {
            if ( _account.LoginClient.LoginInfo.ZrRules != null )
            {
                ZeroRatingMiddleware.Update(_account.LoginClient.LoginInfo.ZrRules);
            }

            handler.Push(ZeroRatingMiddleware, ModifyHeadersMiddleware);
        }

        internal static Task<HttpClient> CreateAsync(Account account)
        {
            var ret = new HttpClient(account);

            return ret.InitializeAsync();
        }

        #endregion

        #region Fields

        private readonly Account _account;

        private HttpClientHandler _handler;

        private WebProxy _proxy;

        private readonly LastAction _lastCookieSave;

        private System.Net.Http.HttpClient _request;

        private readonly SemaphoreSlim _lock;

        #endregion
    }
}