using System.Text.Json;

using Apex.Instagram.API.Response.JsonMap.Model;
using Apex.Instagram.API.Response.Serializer;
using Apex.Instagram.API.Tests.Maps;

using Xunit;

namespace Apex.Instagram.API.Tests
{
    public class SerializerTest
    {
        [Fact]
        public void DictJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"dict\": {\"one\":\"hi\",\"two\":\"bye\"}}";
            var result = JsonSerializer.Deserialize<DurableMap>(input, JsonSerializerDefaultOptions.Instance);

            Assert.Equal(2, result.Dict.Count);
            Assert.Equal("hi", result.Dict["one"]);
            Assert.Equal("bye", result.Dict["two"]);
        }

        [Fact]
        public void Durable_Ulong_Formatter()
        {
            var toDeserialize = "{\"username\":\"bobby\",\"pk\":\"123321\"}";
            var result        = JsonSerializer.Deserialize<User>(toDeserialize, JsonSerializerDefaultOptions.Instance);
            Assert.Equal("bobby", result.Username);
            Assert.Equal(123321u, result.Pk);

            toDeserialize = "{\"username\":\"bobby\",\"pk\":123321}";
            result        = JsonSerializer.Deserialize<User>(toDeserialize, JsonSerializerDefaultOptions.Instance);
            Assert.Equal("bobby", result.Username);
            Assert.Equal(123321u, result.Pk);
        }

        [Fact]
        public void NormalStringAndNumberJsonDeserialize()
        {
            var input  = "{\"number\": 123321123,\"text\": \"heeey\"}";
            var result = JsonSerializer.Deserialize<DurableMap>(input, JsonSerializerDefaultOptions.Instance);

            Assert.Equal(123321123u, result.Number);
            Assert.Equal("heeey", result.Text);
            Assert.Null(result.Dict);
        }

        [Fact]
        public void NumberAsInvalidStringJsonDeserialize()
        {
            var input = "{\"number\": \"blabla\"}";
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<DurableMap>(input, JsonSerializerDefaultOptions.Instance));
        }

        [Fact]
        public void NumberAsStringJsonDeserialize()
        {
            var input  = "{\"number\": \"4445444\"}";
            var result = JsonSerializer.Deserialize<DurableMap>(input, JsonSerializerDefaultOptions.Instance);

            Assert.Equal(4445444u, result.Number);
            Assert.Null(result.Dict);
            Assert.Null(result.Text);
        }

        [Fact]
        public void StringAsBoolJsonDeserialize()
        {
            var input  = "{\"text\": true}";
            var result = JsonSerializer.Deserialize<DurableMap>(input, JsonSerializerDefaultOptions.Instance);

            Assert.Equal("True", result.Text);
            Assert.Null(result.Dict);
            Assert.Null(result.Number);
        }

        [Fact]
        public void StringAsNumberJsonDeserialize()
        {
            var input  = "{\"text\": 4445444}";
            var result = JsonSerializer.Deserialize<DurableMap>(input, JsonSerializerDefaultOptions.Instance);

            Assert.Equal("4445444", result.Text);
            Assert.Null(result.Dict);
            Assert.Null(result.Number);
        }
    }
}