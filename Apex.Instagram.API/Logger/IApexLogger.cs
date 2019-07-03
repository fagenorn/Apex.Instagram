using System;
using System.Net.Http;

using Apex.Instagram.API.Exception;

namespace Apex.Instagram.API.Logger
{
    /// <summary>Logger interface</summary>
    public interface IApexLogger
    {
        /// <summary>Occurs when [log message published].</summary>
        event EventHandler<ApexLogMessagePublishedEventArgs> LogMessagePublished;

        /// <summary>Logs the specified message verbosely.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        void Verbose<TSource>(string message, params object[] parameters);

        /// <summary>Logs the specified message as debug information.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        void Debug<TSource>(string message, params object[] parameters);

        /// <summary>Logs the specified message as debug information.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="message">The message.</param>
        void Debug<TSource>(HttpRequestMessage message);

        /// <summary>Logs the specified message as debug information.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="message">The message.</param>
        void Debug<TSource>(HttpResponseMessage message);

        /// <summary>Logs the specified message as information.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        void Info<TSource>(string message, params object[] parameters);

        /// <summary>Logs the specified exception as a warning.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        void Warning<TSource>(ApexException exception, string message, params object[] parameters);

        /// <summary>Logs the specified message as a warning.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        void Warning<TSource>(string message, params object[] parameters);

        /// <summary>Logs the specified exception as an error.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        void Error<TSource>(ApexException exception, string message, params object[] parameters);

        /// <summary>Logs the specified message as an error.</summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        void Error<TSource>(string message, params object[] parameters);
    }
}