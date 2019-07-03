using Apex.Instagram.API.Exception;

namespace Apex.Instagram.API.Request.Exception
{
    /// <inheritdoc />
    /// <summary>Request exception</summary>
    public class RequestException : ApexException
    {
        internal RequestException() { }

        internal RequestException(string message) : base(message) { }

        internal RequestException(string format, params object[] args) : base(string.Format(format, args)) { }

        internal RequestException(string message, System.Exception innerException) : base(message, innerException) { }

        internal RequestException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }

        internal virtual Response.JsonMap.Response Response { get; set; }

        internal bool HasResponse => Response != null;
    }
}