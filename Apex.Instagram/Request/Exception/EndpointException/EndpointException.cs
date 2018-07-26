namespace Apex.Instagram.Request.Exception.EndpointException
{
    public class EndpointException : RequestException
    {
        public EndpointException() { }

        public EndpointException(string message) : base(message) { }

        public EndpointException(string format, params object[] args) : base(string.Format(format, args)) { }

        public EndpointException(string message, System.Exception innerException) : base(message, innerException) { }

        public EndpointException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}