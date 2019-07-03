using Apex.Instagram.API.Model.Request;

namespace Apex.Instagram.API.Tests
{
    internal static class Constants
    {
        public static Proxy FiddlerProxy { get; } = new Proxy("http://127.0.0.1:8888");
    }
}