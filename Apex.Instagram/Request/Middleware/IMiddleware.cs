using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Apex.Instagram.Request.Middleware
{
    internal interface IMiddleware
    {
        Task<HttpResponseMessage> Invoke(HttpRequestMessage request, Func<HttpRequestMessage, Task<HttpResponseMessage>> next);
    }
}