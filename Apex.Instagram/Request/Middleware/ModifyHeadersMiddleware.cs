using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Apex.Instagram.Request.Middleware
{
    internal class ModifyHeadersMiddleware : IMiddleware
    {
        private readonly string _userAgent;

        public ModifyHeadersMiddleware(string userAgent) { _userAgent = userAgent; }

        public async Task<HttpResponseMessage> Invoke(HttpRequestMessage request, Func<HttpRequestMessage, Task<HttpResponseMessage>> next)
        {
            SetHeader(request.Headers, "User-Agent", _userAgent);
            SetHeader(request.Headers, "X-FB-HTTP-Engine", Constants.Request.Instance.XFbHttpEngine);
            SetHeader(request.Headers, "Accept", "*/*");
            SetHeader(request.Headers, "Accept-Encoding", Constants.Request.Instance.HeaderAcceptEncoding);
            SetHeader(request.Headers, "Accept-Language", Constants.Request.Instance.HeaderAcceptLanguage);

            return await next(request).ConfigureAwait(false);
        }

        private void SetHeader(HttpRequestHeaders headers, string name, string value)
        {
            if ( headers.Contains(name) )
            {
                headers.Remove(name);
            }

            headers.Add(name, value);
        }
    }
}