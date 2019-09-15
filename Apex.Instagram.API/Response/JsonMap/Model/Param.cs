using System.Runtime.Serialization;
using System.Text.Json;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Param
    {
        [DataMember(Name = "name")]
        public JsonElement Name { get; set; }

        [DataMember(Name = "value")]
        public JsonElement Value { get; set; }
    }
}