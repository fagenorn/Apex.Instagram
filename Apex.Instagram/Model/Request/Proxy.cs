using System.Net;

using MessagePack;

namespace Apex.Instagram.Model.Request
{
    [MessagePackObject]
    public class Proxy
    {
        public Proxy(string address, string username = null, string password = null)
        {
            Address  = address;
            Username = username;
            Password = password;
        }

        [Key(0)]
        public string Address { get; }

        [Key(1)]
        public string Username { get; }

        [Key(2)]
        public string Password { get; }

        internal WebProxy GetWebProxy()
        {
            if ( string.IsNullOrWhiteSpace(Username) )
            {
                return new WebProxy(Address, false);
            }

            return new WebProxy(Address, false, null, new NetworkCredential(Username, Password));
        }
    }
}