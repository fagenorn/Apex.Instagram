using System.Runtime.Serialization;

using Apex.Instagram.Request.Exception;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class Response
    {
        public Response(string status, dynamic message, Message[] messages)
        {
            Status   = status;
            Message  = message;
            Messages = messages;
        }

        [DataMember(Name = "status")]
        public string Status { get; }

        [DataMember(Name = "message")]
        public dynamic Message { get; }

        [DataMember(Name = "_messages")]
        public Message[] Messages { get; }

        public string[] GetErrors()
        {
            if ( Message is string )
            {
                return new string[]
                       {
                           Message
                       };
            }

            if ( Message is string[] )
            {
                return Message;
            }

            throw new RequestException("Unable to parse error messages.");
        }
    }
}