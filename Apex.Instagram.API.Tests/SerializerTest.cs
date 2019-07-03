using Apex.Instagram.API.Tests.Maps;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Utf8Json;

namespace Apex.Instagram.API.Tests
{
    [TestClass]
    public class SerializerTest
    {
        [TestMethod]
        public void NormalStringAndNumberJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"number\": 123321123,\"text\": \"heeey\"}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.AreEqual(123321123u, result.Number);
            Assert.AreEqual("heeey", result.Text);
            Assert.IsNull(result.Dict);
        }

        [TestMethod]
        public void NumberAsStringJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"number\": \"4445444\"}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.AreEqual(4445444u, result.Number);
            Assert.IsNull(result.Dict);
            Assert.IsNull(result.Text);
        }

        [TestMethod]
        public void NumberAsInvalidStringJsonDeserialize()
        {
            Initialization.Initialize();
            var input = "{\"number\": \"blabla\"}";
            Assert.ThrowsException<JsonParsingException>(() => JsonSerializer.Deserialize<DurableMap>(input));
        }

        [TestMethod]
        public void StringAsNumberJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"text\": 4445444}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.AreEqual("4445444", result.Text);
            Assert.IsNull(result.Dict);
            Assert.IsNull(result.Number);
        }

        [TestMethod]
        public void StringAsBoolJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"text\": true}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.AreEqual("True", result.Text);
            Assert.IsNull(result.Dict);
            Assert.IsNull(result.Number);
        }

        [TestMethod]
        public void DictJsonDeserialize()
        {
            Initialization.Initialize();
            var input  = "{\"dict\": {\"one\":\"hi\",\"two\":\"bye\"}}";
            var result = JsonSerializer.Deserialize<DurableMap>(input);

            Assert.AreEqual(2, result.Dict.Count);
            Assert.AreEqual("hi", result.Dict["one"]);
            Assert.AreEqual("bye", result.Dict["two"]);
        }
    }
}