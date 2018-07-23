using Apex.Instagram.Exception;

namespace Apex.Instagram.Request.Exception
{
    public class RequestException : ApexException
    {
        public RequestException() { }

        public RequestException(string message) : base(message) { }

        public RequestException(string format, params object[] args) : base(string.Format(format, args)) { }

        public RequestException(string message, System.Exception innerException) : base(message, innerException) { }

        public RequestException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}