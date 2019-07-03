namespace Apex.Instagram.API.Exception
{
    /// <summary>
    ///     <see cref="AccountBuilder" /> exception.
    /// </summary>
    public class AccountBuilderException : ApexException
    {
        internal AccountBuilderException() { }

        internal AccountBuilderException(string message) : base(message) { }

        internal AccountBuilderException(string format, params object[] args) : base(string.Format(format, args)) { }

        internal AccountBuilderException(string message, System.Exception innerException) : base(message, innerException) { }

        internal AccountBuilderException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}