﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
            await _account.Storage.Proxy.SaveAsync(proxy);
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
            await _lock.WaitAsync(ct);
            try
            {
                HttpResponseMessage result;
                _account.Logger.Debug<HttpClient>(request);

                try
                {
                    result = await _request.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct);
                    _account.Logger.Debug<HttpClient>(result);
                }
                catch (HttpRequestException e)
                {
                    if ( e.InnerException is WebException webException && webException.Response is HttpWebResponse exceptionResponse )
                    {
                        result = new HttpResponseMessage(exceptionResponse.StatusCode);
                    }
                    else
                    {
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

                return await ResponseInfo<T>.CreateAsync<T>(result);
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
                    await _account.Storage.Cookie.SaveAsync(_handler.CookieContainer);
                    _lastCookieSave.Update();
                }

                request.Dispose();
                _lock.Release();
            }
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

        #region Properties

        internal ZeroRatingMiddleware ZeroRatingMiddleware { get; }

        internal ModifyHeadersMiddleware ModifyHeadersMiddleware { get; }

        #endregion

        #region Constructor

        private HttpClient(Account account)
        {
            _account                = account;
            _lastCookieSave         = new LastAction(TimeSpan.FromMilliseconds(50)); // Save new cookies only every 50 ms. Reducing this will cause saves to occur more often at the cost of performance.
            _lock                   = new SemaphoreSlim(1);
            ZeroRatingMiddleware    = new ZeroRatingMiddleware();
            ModifyHeadersMiddleware = new ModifyHeadersMiddleware(_account.AccountInfo.DeviceInfo.UserAgent);
        }

        private async Task<HttpClient> InitializeAsync()
        {
            var cookies = await _account.Storage.Cookie.LoadAsync();
            var proxy   = await _account.Storage.Proxy.LoadAsync();

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