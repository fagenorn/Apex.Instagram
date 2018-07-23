namespace Apex.Instagram.Exception
{
    public class ApexException : System.Exception
    {
        public ApexException() { }

        public ApexException(string message) : base(message) { }

        public ApexException(string format, params object[] args) : base(string.Format(format, args)) { }

        public ApexException(string message, System.Exception innerException) : base(message, innerException) { }

        public ApexException(string format, System.Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}