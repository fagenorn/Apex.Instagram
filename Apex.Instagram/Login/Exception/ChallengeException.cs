using Apex.Instagram.Exception;

namespace Apex.Instagram.Login.Exception
{
    public class ChallengeException : ApexException
    {
        public ChallengeException() { }

        public ChallengeException(string message) : base(message) { }

        public ChallengeException(string format, params object[] args) : base(string.Format(format, args)) { }

        public ChallengeException(string message, System.Exception innerException) : base(message, innerException) { }

        public ChallengeException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}