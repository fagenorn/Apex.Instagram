using Apex.Instagram.API.Exception;

namespace Apex.Instagram.API.Login.Exception
{
    /// <summary>Login exception</summary>
    public class LoginException : ApexException
    {
        internal LoginException() { }

        internal LoginException(string message) : base(message) { }

        internal LoginException(string format, params object[] args) : base(string.Format(format, args)) { }

        internal LoginException(string message, System.Exception innerException) : base(message, innerException) { }

        internal LoginException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}