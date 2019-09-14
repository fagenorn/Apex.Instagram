using Apex.Shared.Model;

namespace Apex.Instagram.API.Tests.Extra
{
    internal static class Constants
    {
        public static Proxy FiddlerProxy { get; } = new Proxy("http://127.0.0.1:8888");
    }
}