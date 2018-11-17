namespace Apex.Instagram.Request.Exception.EndpointException
{
    /// <inheritdoc />
    /// <summary>
    ///     Endpoint exception.
    /// </summary>
    public class EndpointException : RequestException
    {
        internal EndpointException() { }

        internal EndpointException(string message) : base(message) { }

        internal EndpointException(string format, params object[] args) : base(string.Format(format, args)) { }

        internal EndpointException(string message, System.Exception innerException) : base(message, innerException) { }

        internal EndpointException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}