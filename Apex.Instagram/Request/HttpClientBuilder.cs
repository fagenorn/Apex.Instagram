using System.Net;

using Apex.Instagram.Model.Request;

namespace Apex.Instagram.Request
{
    internal class HttpClientBuilder
    {
        public HttpClientBuilder(Account account) { _account = account; }

        public HttpClientBuilder SetProxy(Proxy proxy)
        {
            _proxy = proxy;

            return this;
        }

        public HttpClientBuilder SetCookieStorage(CookieContainer cookieContainer)
        {
            _cookieContainer = cookieContainer;

            return this;
        }

        public HttpClient Build() { return new HttpClient(_account, _cookieContainer, _proxy); }

        #region Fields

        private readonly Account _account;

        private CookieContainer _cookieContainer;

        private Proxy _proxy;

        #endregion
    }
}