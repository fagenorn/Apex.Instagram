using Apex.Instagram.API.Response.JsonMap.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Utf8Json;

namespace Apex.Instagram.API.Tests
{
    [TestClass]
    public class JsonTests
    {
        [TestMethod]
        public void Durable_Ulong_Formatter()
        {
            var toDeserialize = "{\"username\":\"bobby\",\"pk\":\"123321\"}";
            var result = JsonSerializer.Deserialize<User>(toDeserialize);
            Assert.AreEqual("bobby", result.Username);
            Assert.AreEqual(123321u, result.Pk);
        
            toDeserialize = "{\"username\":\"bobby\",\"pk\":123321}";
            result         = JsonSerializer.Deserialize<User>(toDeserialize);
            Assert.AreEqual("bobby", result.Username);
            Assert.AreEqual(123321u, result.Pk);
        }
    }
}