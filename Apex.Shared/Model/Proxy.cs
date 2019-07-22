using System;
using System.Net;
using System.Runtime.Serialization;

namespace Apex.Shared.Model
{
    /// <summary>Account proxy.</summary>
    [DataContract]
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
        [DataMember(Order = 0)]
        public string Address { get; }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [DataMember(Order = 1)]
        public string Username { get; }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [DataMember(Order = 2)]
        public string Password { get; }

        /// <summary>Gets a value indicating whether this instance has credentials.</summary>
        /// <value>
        ///     <c>true</c> if this instance has credentials; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool HasCredentials => !string.IsNullOrWhiteSpace(Username);

        /// <summary>Gets the credentials.</summary>
        /// <value>The credentials.</value>
        [IgnoreDataMember]
        public ICredentials Credentials => new NetworkCredential(Username, Password);

        /// <summary>Gets the URI.</summary>
        /// <value>The URI.</value>
        [IgnoreDataMember]
        public Uri Uri { get; }
    }
}