using System;
using System.Net.Http;
using System.Threading.Tasks;

using Apex.Instagram.API.Exception;

namespace Apex.Instagram.API.Logger
{
    internal sealed class NullLogger : IApexLogger
    {
#pragma warning disable 0067
        public event EventHandler<ApexLogMessagePublishedEventArgs> LogMessagePublished;
#pragma warning restore 0067
        public void Verbose<TSource>(string message, params object[] parameters) { }

        public void Debug<TSource>(string message, params object[] parameters) { }

        public Task Debug<TSource>(HttpRequestMessage message) { return Task.CompletedTask; }

        public Task Debug<TSource>(HttpResponseMessage message) { return Task.CompletedTask; }

        public void Info<TSource>(string message, params object[] parameters) { }

        public void Warning<TSource>(ApexException exception, string message, params object[] parameters) { }

        public void Warning<TSource>(string message, params object[] parameters) { }

        public void Error<TSource>(ApexException exception, string message, params object[] parameters) { }

        public void Error<TSource>(string message, params object[] parameters) { }
    }
}