using System;

namespace Apex.Instagram.Logger
{
    /// <summary>Log message event arguments.</summary>
    public class ApexLogMessagePublishedEventArgs : EventArgs
    {
        /// <summary>Initializes a new instance of the <see cref="ApexLogMessagePublishedEventArgs" /> class.</summary>
        /// <param name="logMessage">The log message.</param>
        /// <exception cref="ArgumentNullException">logMessage</exception>
        public ApexLogMessagePublishedEventArgs(ApexLogMessage logMessage) { TraceMessage = logMessage ?? throw new ArgumentNullException(nameof(logMessage)); }

        /// <summary>Gets the trace message.</summary>
        /// <value>The trace message.</value>
        public ApexLogMessage TraceMessage { get; }
    }
}