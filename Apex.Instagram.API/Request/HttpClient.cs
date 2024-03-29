﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.API.Constants;
using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Request.Middleware;
using Apex.Instagram.API.Response;
using Apex.Instagram.API.Utils;
using Apex.Shared.Model;

namespace Apex.Instagram.API.Request
{
    internal class HttpClient : IDisposable
    {
        public void Dispose()
        {
            _request?.Dispose();
            _handler?.Dispose();
        }

        public void StartEmulatingBatch()
        {
            _pidgeonBatch     = true;
            _pidgeonTimestamp = Epoch.Current;
        }

        public void StopEmulatingBatch() { _pidgeonBatch = false; }

        public async Task UpdateProxy(Proxy proxy)
        {
            if ( proxy == null )
            {
                _proxy.Credentials = null;
            }
            else
            {
                _proxy.Credentials = proxy.HasCredentials ? proxy.Credentials : null;
            }

            _proxy.Address = proxy?.Uri;
            await _account.Storage.Proxy.SaveAsync(proxy)
                          .ConfigureAwait(false);
        }

        /// <summary>
        ///     Makes a request using the open connection.
        ///     IMPORTANT: Dispose reponse.
        /// </summary>
        /// <param name="builder">The request builder. Will be disposed after request has been made.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Make sure to dispose <see cref="HttpResponseMessage" /> or a memory leak can occur.</returns>
        public async Task<ResponseInfo<T>> GetResponseAsync<T>(RequestBuilder builder, CancellationToken ct = default) where T : Response.JsonMap.Response
        {
            await _lock.WaitAsync(ct)
                       .ConfigureAwait(false);

            try
            {
                return await GetResponseAsyncInternal<T>(builder, ct)
                           .ConfigureAwait(false);
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
                    await _account.Storage.Cookie.SaveAsync(_handler.CookieContainer)
                                  .ConfigureAwait(false);

                    _lastCookieSave.Update();
                }

                _lock.Release();
            }
        }

        private async Task<ResponseInfo<T>> GetResponseAsyncInternal<T>(RequestBuilder builder, CancellationToken ct, int depth = 1) where T : Response.JsonMap.Response
        {
            HttpResponseMessage result;
            var                 request = builder.Build();
            await _account.Logger.Debug<HttpClient>(request)
                          .ConfigureAwait(false);

            try
            {
                result = await _request.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct)
                                       .ConfigureAwait(false);

                await _account.Logger.Debug<HttpClient>(result)
                              .ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                switch ( e.InnerException )
                {
                    case WebException webException when webException.Response is HttpWebResponse exceptionResponse:
                        result = new HttpResponseMessage(exceptionResponse.StatusCode);

                        break;
                    case IOException _ when depth < Constants.Request.Instance.MaxRequestRetries:

                        await Task.Delay(1500, ct);

                        return await GetResponseAsyncInternal<T>(builder, ct, depth + 1)
                                   .ConfigureAwait(false);
                    default:

                        throw new RequestException("Critical request error occured.", e);
                }
            }
            catch (OperationCanceledException e)
            {
                throw new RequestException("Request has been cancelled.", e);
            }
            finally
            {
                request.Dispose();
                builder.Dispose();
            }

            return await ResponseInfo<T>.CreateAsync<T>(result)
                                        .ConfigureAwait(false);
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
            _lastCookieSave         = new LastAction(Delays.Instance.CookieSaveInterval);
            _lock                   = new SemaphoreSlim(1);
            ZeroRatingMiddleware    = new ZeroRatingMiddleware();
            ModifyHeadersMiddleware = new ModifyHeadersMiddleware(Constants.Request.Instance.PermanentHeaders);

            ModifyHeadersMiddleware.AddHeader("X-Pigeon-Session-Id", Utils.Instagram.Instance.GenerateUuid());
            ModifyHeadersMiddleware.AddHeader("X-Pigeon-Rawclienttime", GetPidgeonRawClientTime);
            ModifyHeadersMiddleware.AddHeader("User-Agent", _account.AccountInfo.DeviceInfo.UserAgent);
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

        private string GetPidgeonRawClientTime()
        {
            double result;
            if ( _pidgeonBatch )
            {
                result            =  _pidgeonTimestamp;
                _pidgeonTimestamp += Randomizer.Instance.Number(101) / 1000.0;
            }
            else
            {
                result = Epoch.Current;
            }

            return result.ToString("F3");
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

        private bool _pidgeonBatch;

        private double _pidgeonTimestamp;

        #endregion
    }
}