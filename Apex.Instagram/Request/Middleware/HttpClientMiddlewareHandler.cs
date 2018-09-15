using System;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Apex.Instagram.Request.Middleware
{
    internal class HttpClientMiddlewareHandler : DelegatingHandler
    {
        private readonly AsyncLocal<ImmutableStack<IMiddleware>> _data = new AsyncLocal<ImmutableStack<IMiddleware>>();

        public HttpClientMiddlewareHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

        public HttpClientMiddlewareHandler() : base(new HttpClientHandler()) { }

        private ImmutableStack<IMiddleware> Middlewares
        {
            get
            {
                var middlewares = _data.Value;
                if ( middlewares != null )
                {
                    return middlewares;
                }

                middlewares = ImmutableStack<IMiddleware>.Empty;
                _data.Value = middlewares;

                return middlewares;
            }
            set => _data.Value = value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var func = ComposedMiddleware(req => base.SendAsync(req, cancellationToken));

            return func(request);
        }

        public Func<HttpRequestMessage, Task<HttpResponseMessage>> ComposedMiddleware(Func<HttpRequestMessage, Task<HttpResponseMessage>> baseFunc)
        {
            return Middlewares.Reverse()
                              .Aggregate(baseFunc, (fn, middleware) => req => middleware.Invoke(req, fn));
        }

        public HttpMessageHandler Handler() { return this; }

        public IDisposable Push(params IMiddleware[] middlewares)
        {
            if ( middlewares == null )
            {
                throw new ArgumentNullException(nameof(middlewares));
            }

            var bookmark = new ContextStackBookmark(Middlewares, this);
            Middlewares = middlewares.Aggregate(Middlewares, (current, handler) => current.Push(handler));

            return bookmark;
        }

        private sealed class ContextStackBookmark : IDisposable
        {
            private readonly ImmutableStack<IMiddleware> _bookmark;

            private readonly HttpClientMiddlewareHandler _httpClientMiddlewareHandler;

            public ContextStackBookmark(ImmutableStack<IMiddleware> bookmark, HttpClientMiddlewareHandler clientMiddlewareHandler)
            {
                _httpClientMiddlewareHandler = clientMiddlewareHandler;
                _bookmark                    = bookmark;
            }

            public void Dispose() { _httpClientMiddlewareHandler.Middlewares = _bookmark; }
        }
    }
}