using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using Apex.Instagram.Request.Exception;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public abstract class Response
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public dynamic Message { get; set; }

        [DataMember(Name = "_messages")]
        public Message[] Messages { get; set; }

        public string[] GetErrors()
        {
            if ( Message == null )
            {
                throw new RequestException("No error object found.");
            }

            if ( Message is string s )
            {
                return new[]
                       {
                           s
                       };
            }

            if ( Message.ContainsKey("errors") && Message["errors"] is IList<object> )
            {
                return ((IList<object>) Message["errors"]).Cast<string>()
                                                          .ToArray();
            }

            throw new RequestException("Unable to parse error message.");
        }

        public bool IsOk() { return Status == Constants.Response.Instance.StatusOk; }
    }
}