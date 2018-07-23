using System;
using System.Net.Http;

using Apex.Instagram.Exception;

namespace Apex.Instagram.Logger
{
    public interface IApexLogger
    {
        event EventHandler<ApexLogMessagePublishedEventArgs> LogMessagePublished;

        void Verbose<TSource>(string message, params object[] parameters);

        void Debug<TSource>(string message, params object[] parameters);

        void Debug<TSource>(HttpRequestMessage message);

        void Debug<TSource>(HttpResponseMessage message);

        void Info<TSource>(string message, params object[] parameters);

        void Warning<TSource>(ApexException exception, string message, params object[] parameters);

        void Warning<TSource>(string message, params object[] parameters);

        void Error<TSource>(ApexException exception, string message, params object[] parameters);

        void Error<TSource>(string message, params object[] parameters);
    }
}