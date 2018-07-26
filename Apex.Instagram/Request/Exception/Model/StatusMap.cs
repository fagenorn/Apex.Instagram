namespace Apex.Instagram.Request.Exception.Model
{
    internal class StatusMap<T> : IStatusCodeMap where T : RequestException, new()
    {
        private readonly int _statusCode;

        public StatusMap(int statusCode) { _statusCode = statusCode; }

        public bool TryGet(int message, out RequestException result)
        {
            if ( _statusCode == message )
            {
                result = new T();

                return true;
            }

            result = null;

            return false;
        }
    }
}