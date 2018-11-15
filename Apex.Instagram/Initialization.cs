using MessagePack;

using Utf8Json;

using JsonResolver = Apex.Instagram.Response.Serializer;
using MessagePackResolver = Apex.Instagram.Storage.Serializer.MessagePackFormatter;

namespace Apex.Instagram
{
    internal static class Initialization
    {
        private static bool _done;

        public static void Initialize()
        {
            if ( _done )
            {
                return;
            }

            JsonSerializer.SetDefaultResolver(JsonResolver.CustomCompositeResolver.Instance);
            MessagePackSerializer.SetDefaultResolver(MessagePackResolver.CustomCompositeResolver.Instance);

            _done = true;
        }
    }
}