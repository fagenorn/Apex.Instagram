using System;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Apex.Instagram.API.Request.Middleware
{
    internal class HttpClientMiddlewareHandler : DelegatingHandler
    {
        private ImmutableStack<IMiddleware> _data;

        public HttpClientMiddlewareHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

        public HttpClientMiddlewareHandler() : base(new HttpClientHandler()) { }

        private ImmutableStack<IMiddleware> Middlewares
        {
            get
            {
                var middlewares = _data;
                if ( middlewares != null )
                {
                    return middlewares;
                }

                middlewares = ImmutableStack<IMiddleware>.Empty;
                _data       = middlewares;

                return middlewares;
            }
            set => _data = value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var func = ComposedMiddleware(req => base.SendAsync(req, cancellationToken));

            return func(request);
        }

        public Func<HttpRequestMessage, Task<HttpResponseMessage>> ComposedMiddleware(Func<HttpRequestMessage, Task<HttpResponseMessage>> baseFunc)
        {
            return Middlewares.Reverse().Aggregate(baseFunc, (fn, middleware) => req => middleware.Invoke(req, fn));
        }

        public HttpMessageHandler Handler() { return this; }

        public void Push(params IMiddleware[] middlewares)
        {
            if ( middlewares == null )
            {
                throw new ArgumentNullException(nameof(middlewares));
            }

            Middlewares = middlewares.Aggregate(Middlewares, (current, handler) => current.Push(handler));
        }
    }
}