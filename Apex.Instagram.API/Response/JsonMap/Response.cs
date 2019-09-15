using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Response.JsonMap.Model;
using Apex.Instagram.API.Response.Serializer;

namespace Apex.Instagram.API.Response.JsonMap
{
    public abstract class Response
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public JsonElement Message { get; set; }

        [DataMember(Name = "_messages")]
        [JsonPropertyName("_messages")]
        public Message[] Messages { get; set; }

        public string[] GetErrors()
        {
            switch ( Message.ValueKind )
            {
                case JsonValueKind.String:
                    return new[]
                           {
                               Message.GetString()
                           };
                case JsonValueKind.Object:
                {
                    var errorProperty = Message.GetProperty("errors");

                    if ( errorProperty.ValueKind != JsonValueKind.Array )
                    {
                        throw new RequestException("Unable to parse error message.");
                    }

                    var errorEnumerator = errorProperty.EnumerateArray();

                    return errorEnumerator.Select(x => x.GetString())
                                          .ToArray();
                }
                default:
                    throw new RequestException("No error object found.");
            }
        }

        public bool IsOk() { return Status == Constants.Response.Instance.StatusOk; }

        public override string ToString() { return JsonSerializer.Serialize(this, JsonSerializerDefaultOptions.Instance); }
    }
}