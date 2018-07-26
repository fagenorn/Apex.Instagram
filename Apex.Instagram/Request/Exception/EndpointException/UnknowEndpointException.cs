namespace Apex.Instagram.Request.Exception.EndpointException
{
    public class UnknowEndpointException : EndpointException
    {
        public UnknowEndpointException() { }

        public UnknowEndpointException(string message) : base(message) { }

        public UnknowEndpointException(string format, params object[] args) : base(string.Format(format, args)) { }

        public UnknowEndpointException(string message, System.Exception innerException) : base(message, innerException) { }

        public UnknowEndpointException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}