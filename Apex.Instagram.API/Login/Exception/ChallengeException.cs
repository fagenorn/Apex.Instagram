using Apex.Instagram.API.Exception;

namespace Apex.Instagram.API.Login.Exception
{
    /// <summary>Challenge exception</summary>
    public class ChallengeException : ApexException
    {
        internal ChallengeException() { }

        internal ChallengeException(string message) : base(message) { }

        internal ChallengeException(string format, params object[] args) : base(string.Format(format, args)) { }

        internal ChallengeException(string message, System.Exception innerException) : base(message, innerException) { }

        internal ChallengeException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}