namespace Apex.Instagram.API.Exception
{
    /// <summary>
    ///     Apex exception.
    /// </summary>
    public class ApexException : System.Exception
    {
        internal ApexException() { }

        internal ApexException(string message) : base(message) { }

        internal ApexException(string format, params object[] args) : base(string.Format(format, args)) { }

        internal ApexException(string message, System.Exception innerException) : base(message, innerException) { }

        internal ApexException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}