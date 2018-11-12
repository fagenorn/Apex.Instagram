using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Apex.Instagram.Request.Middleware
{
    internal class ModifyHeadersMiddleware : IMiddleware
    {
        private readonly Dictionary<string, string> _headers;

        public ModifyHeadersMiddleware(Dictionary<string, string> headers) { _headers = headers; }

        public async Task<HttpResponseMessage> Invoke(HttpRequestMessage request, Func<HttpRequestMessage, Task<HttpResponseMessage>> next)
        {
            foreach (var header in _headers)
            {
                SetHeader(request.Headers, header.Key, header.Value);
            }

            return await next(request).ConfigureAwait(false);
        }

        public void AddHeader(string key, string name)
        {
            if (_headers.ContainsKey(name))
            {
                _headers.Remove(name);
            }

            _headers.Add(key, name);
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