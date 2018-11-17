using System;

using Apex.Instagram.Exception;

namespace Apex.Instagram.Logger
{
    /// <summary>Log message.</summary>
    public class ApexLogMessage
    {
        /// <summary>Initializes a new instance of the <see cref="ApexLogMessage" /> class.</summary>
        /// <param name="logId">The log identifier.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="source">The source.</param>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public ApexLogMessage(string logId, DateTime timestamp, int threadId, string source, ApexLogLevel level, string message, ApexException exception)
        {
            LogId     = logId;
            Timestamp = timestamp;
            ThreadId  = threadId;
            Source    = source;
            Level     = level;
            Message   = message;
            Exception = exception;
        }

        /// <summary>Gets the log identifier.</summary>
        /// <value>The log identifier.</value>
        public string LogId { get; }

        /// <summary>Gets the timestamp.</summary>
        /// <value>The timestamp.</value>
        public DateTime Timestamp { get; }

        /// <summary>Gets the thread identifier.</summary>
        /// <value>The thread identifier.</value>
        public int ThreadId { get; }

        /// <summary>Gets the source.</summary>
        /// <value>The source.</value>
        public string Source { get; }

        /// <summary>Gets the level.</summary>
        /// <value>The level.</value>
        public ApexLogLevel Level { get; }

        /// <summary>Gets the message.</summary>
        /// <value>The message.</value>
        public string Message { get; }

        /// <summary>Gets the exception.</summary>
        /// <value>The exception.</value>
        public ApexException Exception { get; }

        /// <summary>Returns a <see cref="System.String" /> that represents this instance.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var result = $"[{Timestamp:O}] [Id:{LogId}] [Thread:{ThreadId}] [Source:{Source}] [{Level}]: {Message}";
            if ( Exception != null )
            {
                result += Environment.NewLine + Exception + Environment.NewLine;
            }

            return result;
        }
    }
}