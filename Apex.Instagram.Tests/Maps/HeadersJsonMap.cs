using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Apex.Instagram.Tests.Maps
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class HeadersJsonMap
    {
        public HeadersJsonMap(Dictionary<string, string> headers) { this.headers = headers; }

        public Dictionary<string, string> headers { get; }
    }
}