using System.Collections.Generic;
using System.Linq;

using Apex.Instagram.Utils;

namespace Apex.Instagram.Request.Exception.Model
{
    internal class ExceptionMap<T> : IExceptionMap where T : RequestException, new()
    {
        private readonly List<string> _patterns;

        public ExceptionMap(params string[] patterns) { _patterns = new List<string>(patterns); }

        public bool TryGet(string message, out RequestException result)
        {
            if (_patterns.Any(x => x.EqualsWildcard(message)))
            {
                result = new T();

                return true;
            }

            result = null;

            return false;
        }
    }
}