using Apex.Instagram.API.Exception;

namespace Apex.Instagram.API.Request.Exception
{
    public class RequestBuilderException : ApexException
    {
        public RequestBuilderException() { }

        public RequestBuilderException(string message) : base(message) { }

        public RequestBuilderException(string format, params object[] args) : base(string.Format(format, args)) { }

        public RequestBuilderException(string message, System.Exception innerException) : base(message, innerException) { }

        public RequestBuilderException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}