using System;

using Apex.Instagram.Exception;

namespace Apex.Instagram.Logger
{
    public class ApexLogMessage
    {
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

        public string LogId { get; }

        public DateTime Timestamp { get; }

        public int ThreadId { get; }

        public string Source { get; }

        public ApexLogLevel Level { get; }

        public string Message { get; }

        public ApexException Exception { get; }

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