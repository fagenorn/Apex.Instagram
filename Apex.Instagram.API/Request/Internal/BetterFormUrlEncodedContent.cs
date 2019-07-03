using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Apex.Instagram.API.Request.Internal
{
    internal class BetterFormUrlEncodedContent : ByteArrayContent
    {
        public BetterFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection) : base(GetContentByteArray(nameValueCollection))
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
                                  {
                                      CharSet = "UTF-8"
                                  };
        }

        private static byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if ( nameValueCollection == null )
            {
                throw new ArgumentNullException(nameof(nameValueCollection));
            }

            var stringBuilder = new StringBuilder();
            foreach ( var current in nameValueCollection )
            {
                if ( stringBuilder.Length > 0 )
                {
                    stringBuilder.Append('&');
                }

                stringBuilder.Append(Encode(current.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(Encode(current.Value));
            }

            return Encoding.Default.GetBytes(stringBuilder.ToString());
        }

        private static string Encode(string data)
        {
            if ( string.IsNullOrEmpty(data) )
            {
                return string.Empty;
            }

            return WebUtility.UrlEncode(data)
                             ?.Replace("%20", "+");
        }
    }
}