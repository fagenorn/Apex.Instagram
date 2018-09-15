using Apex.Instagram.Exception;

namespace Apex.Instagram.Login.Exception
{
    public class LoginException : ApexException
    {
        public LoginException() { }

        public LoginException(string message) : base(message) { }

        public LoginException(string format, params object[] args) : base(string.Format(format, args)) { }

        public LoginException(string message, System.Exception innerException) : base(message, innerException) { }

        public LoginException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}