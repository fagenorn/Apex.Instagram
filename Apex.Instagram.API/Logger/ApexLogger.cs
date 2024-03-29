﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Apex.Instagram.API.Exception;
using Apex.Instagram.API.Response.Serializer;

namespace Apex.Instagram.API.Logger
{
    /// <inheritdoc />
    /// <summary>Build in Apex logger.</summary>
    public class ApexLogger : IApexLogger
    {
        private readonly string _logId;

        private readonly ApexLogLevel _logLevel;

        /// <summary>Initializes a new instance of the <see cref="ApexLogger" /> class.</summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="logId">The log identifier.</param>
        public ApexLogger(ApexLogLevel logLevel, string logId = null)
        {
            _logLevel = logLevel;
            _logId    = logId;
        }

        /// <inheritdoc />
        public event EventHandler<ApexLogMessagePublishedEventArgs> LogMessagePublished;

        /// <inheritdoc />
        public void Verbose<TSource>(string message, params object[] parameters)
        {
            if ( _logLevel < ApexLogLevel.Verbose )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"{message}");
            Publish<TSource>(ApexLogLevel.Verbose, null, logMessage.ToString(), parameters);
        }

        /// <inheritdoc />
        public void Debug<TSource>(string message, params object[] parameters)
        {
            if ( _logLevel < ApexLogLevel.Debug )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"{message}");
            Publish<TSource>(ApexLogLevel.Debug, null, logMessage.ToString(), parameters);
        }

        /// <inheritdoc />
        public async Task Debug<TSource>(HttpRequestMessage message)
        {
            if ( _logLevel < ApexLogLevel.Debug )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"Request: {message.Method} {message.RequestUri}");
            WriteHeaders(logMessage, message.Headers);
            WriteProperties(logMessage, message.Properties);
            await WriteContent(logMessage, message.Content)
                .ConfigureAwait(false);

            Publish<TSource>(ApexLogLevel.Debug, null, logMessage.ToString(), null);
        }

        /// <inheritdoc />
        public async Task Debug<TSource>(HttpResponseMessage message)
        {
            if ( _logLevel < ApexLogLevel.Debug )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"Response: {message.RequestMessage.Method} {(int) message.StatusCode} {message.RequestMessage.RequestUri}");
            await WriteContent(logMessage, message.Content)
                .ConfigureAwait(false);

            Publish<TSource>(ApexLogLevel.Debug, null, logMessage.ToString(), null);
        }

        /// <inheritdoc />
        public void Info<TSource>(string message, params object[] parameters)
        {
            if ( _logLevel < ApexLogLevel.Info )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"{message}");
            Publish<TSource>(ApexLogLevel.Info, null, logMessage.ToString(), parameters);
        }

        /// <inheritdoc />
        public void Warning<TSource>(ApexException exception, string message, params object[] parameters)
        {
            if ( _logLevel < ApexLogLevel.Warning )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"{message}");
            Publish<TSource>(ApexLogLevel.Warning, exception, logMessage.ToString(), parameters);
        }

        /// <inheritdoc />
        public void Warning<TSource>(string message, params object[] parameters)
        {
            if ( _logLevel < ApexLogLevel.Warning )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"{message}");
            Publish<TSource>(ApexLogLevel.Warning, null, logMessage.ToString(), parameters);
        }

        /// <inheritdoc />
        public void Error<TSource>(ApexException exception, string message, params object[] parameters)
        {
            if ( _logLevel < ApexLogLevel.Error )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"{message}");
            Publish<TSource>(ApexLogLevel.Error, exception, logMessage.ToString(), parameters);
        }

        /// <inheritdoc />
        public void Error<TSource>(string message, params object[] parameters)
        {
            if ( _logLevel < ApexLogLevel.Error )
            {
                return;
            }

            var logMessage = new StringBuilder();
            WriteSeprator(logMessage);
            logMessage.AppendLine($"{message}");
            Publish<TSource>(ApexLogLevel.Error, null, logMessage.ToString(), parameters);
        }

        private void WriteSeprator(StringBuilder sb)
        {
            sb.AppendLine();

            for ( var i = 0; i < 30; i++ )
            {
                sb.Append("-");
            }

            sb.AppendLine();
        }

        private void WriteHeaders(StringBuilder sb, HttpHeaders headers)
        {
            if ( headers == null )
            {
                return;
            }

            if ( !headers.Any() )
            {
                return;
            }

            sb.AppendLine("Headers:");
            foreach ( var item in headers )
            {
                sb.AppendLine($"{item.Key}:{JsonSerializer.Serialize(item.Value, JsonSerializerDefaultOptions.Instance)}");
            }
        }

        private void WriteProperties(StringBuilder sb, IDictionary<string, object> properties)
        {
            if ( properties == null )
            {
                return;
            }

            if ( properties.Count == 0 )
            {
                return;
            }

            sb.AppendLine($"Properties:{Environment.NewLine}{JsonSerializer.Serialize(properties, JsonSerializerDefaultOptions.Instance)}");
        }

        private async Task WriteContent(StringBuilder sb, HttpContent content)
        {
            if ( content == null )
            {
                return;
            }

            sb.AppendLine();
            sb.AppendLine("Content:");
            var raw = await content.ReadAsStringAsync()
                                   .ConfigureAwait(false);

            raw = raw.Contains("<!DOCTYPE html>") ? "<HTML content>" : raw;

            try
            {
                // var pretty = JsonSerializer.PrettyPrint(raw);
                var pretty = raw;
                if ( string.IsNullOrWhiteSpace(pretty) )
                {
                    if ( raw.StartsWith("ig_sig_key_version") )
                    {
                        var temp = WebUtility.UrlDecode(raw.Split(new[]
                                                                  {
                                                                      '.'
                                                                  }, 2)[1]);

                        // sb.AppendLine(JsonSerializer.PrettyPrint(temp));
                        sb.AppendLine(temp);
                    }
                    else
                    {
                        sb.AppendLine(WebUtility.UrlDecode(raw));
                    }
                }
                else
                {
                    sb.AppendLine(pretty);
                }
            }
            catch
            {
                sb.AppendLine(raw);
            }
        }

        private void Publish<TSource>(ApexLogLevel logLevel, ApexException exception, string message, object[] parameters)
        {
            var hasLocalListeners = LogMessagePublished != null;
            if ( !hasLocalListeners )
            {
                return;
            }

            if ( parameters?.Length > 0 )
            {
                message = string.Format(message, parameters);
            }

            var traceMessage = new ApexLogMessage(_logId, DateTime.Now, Environment.CurrentManagedThreadId, typeof(TSource).Name, logLevel, message, exception);

            LogMessagePublished?.Invoke(this, new ApexLogMessagePublishedEventArgs(traceMessage));
        }
    }
}