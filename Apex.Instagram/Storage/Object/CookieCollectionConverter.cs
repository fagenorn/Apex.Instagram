using System.Collections.Generic;
using System.Net;

using MessagePack;

namespace Apex.Instagram.Storage.Object
{
    [MessagePackObject]
    internal class CookieCollectionConverter
    {
        public CookieCollectionConverter(HashSet<Cookie> cookies) { Cookies = cookies; }

        [Key(0)]
        public HashSet<Cookie> Cookies { get; }

        public static implicit operator CookieContainer(CookieCollectionConverter cookieCollectionConverter)
        {
            var cookieContainer = new CookieContainer();

            if ( cookieCollectionConverter != null )
            {
                foreach ( var cookie in cookieCollectionConverter.Cookies )
                {
                    cookieContainer.Add(cookie);
                }
            }

            return cookieContainer;
        }

        public static implicit operator CookieCollectionConverter(CookieContainer cookieContainer)
        {
            var cookies = new HashSet<Cookie>();
            foreach ( var uri in Constants.Request.Instance.CookieUrl )
            {
                foreach ( Cookie cookie in cookieContainer.GetCookies(uri) )
                {
                    cookies.Add(cookie);
                }
            }

            return new CookieCollectionConverter(cookies);
        }
    }
}