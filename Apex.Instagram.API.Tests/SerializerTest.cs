using Apex.Instagram.API.Response.JsonMap.Model;
using Apex.Instagram.API.Tests.Maps;

using Utf8Json;

using Xunit;

namespace Apex.Instagram.API.Tests
{ public class SerializerTest
    {
        [Fact]
        public void NormalStringAndNumberJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"number\": 123321123,\"text\": \"heeey\"}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.Equal(123321123u, result.Number);
            Assert.Equal("heeey", result.Text);
            Assert.Null(result.Dict);
        }

        [Fact]
        public void NumberAsStringJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"number\": \"4445444\"}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.Equal(4445444u, result.Number);
            Assert.Null(result.Dict);
            Assert.Null(result.Text);
        }

        [Fact]
        public void NumberAsInvalidStringJsonDeserialize()
        {
            Initialization.Initialize();
            var input = "{\"number\": \"blabla\"}";
            Assert.Throws<JsonParsingException>(() => JsonSerializer.Deserialize<DurableMap>(input));
        }

        [Fact]
        public void StringAsNumberJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"text\": 4445444}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.Equal("4445444", result.Text);
            Assert.Null(result.Dict);
            Assert.Null(result.Number);
        }

        [Fact]
        public void StringAsBoolJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"text\": true}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.Equal("True", result.Text);
            Assert.Null(result.Dict);
            Assert.Null(result.Number);
        }

        [Fact]
        public void DictJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"dict\": {\"one\":\"hi\",\"two\":\"bye\"}}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.Equal(2, result.Dict.Count);
            Assert.Equal("hi", result.Dict["one"]);
            Assert.Equal("bye", result.Dict["two"]);
        }

        [Fact]
        public void Durable_Ulong_Formatter()
        {
            Initialization.Initialize();
            var toDeserialize = "{\"username\":\"bobby\",\"pk\":\"123321\"}";
            var result        = JsonSerializer.Deserialize<User>(toDeserialize);
            Assert.Equal("bobby", result.Username);
            Assert.Equal(123321u, result.Pk);

            toDeserialize = "{\"username\":\"bobby\",\"pk\":123321}";
            result        = JsonSerializer.Deserialize<User>(toDeserialize);
            Assert.Equal("bobby", result.Username);
            Assert.Equal(123321u, result.Pk);
        }
    }
}