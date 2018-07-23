using System;

namespace Apex.Instagram.Logger
{
    public class ApexLogMessagePublishedEventArgs : EventArgs
    {
        public ApexLogMessagePublishedEventArgs(ApexLogMessage logMessage) { TraceMessage = logMessage ?? throw new ArgumentNullException(nameof(logMessage)); }

        public ApexLogMessage TraceMessage { get; }
    }
}