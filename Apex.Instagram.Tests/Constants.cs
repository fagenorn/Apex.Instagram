using Apex.Instagram.Model.Request;

namespace Apex.Instagram.Tests
{
    internal static class Constants
    {
        public static Proxy FiddlerProxy { get; } = new Proxy("http://127.0.0.1:8888");
    }
}