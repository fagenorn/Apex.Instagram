namespace Apex.Instagram.Exception
{
    public class AccountBuilderException : ApexException
    {
        public AccountBuilderException() { }

        public AccountBuilderException(string message) : base(message) { }

        public AccountBuilderException(string format, params object[] args) : base(string.Format(format, args)) { }

        public AccountBuilderException(string message, System.Exception innerException) : base(message, innerException) { }

        public AccountBuilderException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}