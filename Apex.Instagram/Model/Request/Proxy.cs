using System;
using System.Net;

using MessagePack;

namespace Apex.Instagram.Model.Request
{
    /// <summary>Account proxy.</summary>
    [MessagePackObject]
    public class Proxy
    {
        /// <summary>Initializes a new instance of the <see cref="Proxy" /> class.</summary>
        /// <param name="address">The address.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public Proxy(string address, string username = null, string password = null)
        {
            Address  = address;
            Username = username;
            Password = password;

            Uri = new Uri(Address);
        }

        /// <summary>Gets the address.</summary>
        /// <value>The address.</value>
        [Key(0)]
        public string Address { get; }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [Key(1)]
        public string Username { get; }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [Key(2)]
        public string Password { get; }

        /// <summary>Gets a value indicating whether this instance has credentials.</summary>
        /// <value>
        ///     <c>true</c> if this instance has credentials; otherwise, <c>false</c>.
        /// </value>
        [IgnoreMember]
        public bool HasCredentials => !string.IsNullOrWhiteSpace(Username);

        /// <summary>Gets the credentials.</summary>
        /// <value>The credentials.</value>
        [IgnoreMember]
        public ICredentials Credentials => new NetworkCredential(Username, Password);

        /// <summary>Gets the URI.</summary>
        /// <value>The URI.</value>
        [IgnoreMember]
        public Uri Uri { get; }
    }
}