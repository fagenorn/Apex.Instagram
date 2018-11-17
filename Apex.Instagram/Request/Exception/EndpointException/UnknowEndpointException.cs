namespace Apex.Instagram.Request.Exception.EndpointException
{
    /// <inheritdoc />
    /// <summary>
    ///     Unknow endpoint exception.
    /// </summary>
    public class UnknowEndpointException : EndpointException
    {
        internal UnknowEndpointException() { }

        internal UnknowEndpointException(string message) : base(message) { }

        internal UnknowEndpointException(string format, params object[] args) : base(string.Format(format, args)) { }

        internal UnknowEndpointException(string message, System.Exception innerException) : base(message, innerException) { }

        internal UnknowEndpointException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}