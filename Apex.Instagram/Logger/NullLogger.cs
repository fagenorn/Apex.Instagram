using System;
using System.Net.Http;

using Apex.Instagram.Exception;

namespace Apex.Instagram.Logger
{
    internal sealed class NullLogger : IApexLogger
    {
#pragma warning disable 0067
        public event EventHandler<ApexLogMessagePublishedEventArgs> LogMessagePublished;
#pragma warning restore 0067
        public void Verbose<TSource>(string message, params object[] parameters) { }

        public void Debug<TSource>(string message, params object[] parameters) { }

        public void Debug<TSource>(HttpRequestMessage message) { }

        public void Debug<TSource>(HttpResponseMessage message) { }

        public void Info<TSource>(string message, params object[] parameters) { }

        public void Warning<TSource>(ApexException exception, string message, params object[] parameters) { }

        public void Warning<TSource>(string message, params object[] parameters) { }

        public void Error<TSource>(ApexException exception, string message, params object[] parameters) { }

        public void Error<TSource>(string message, params object[] parameters) { }
    }
}