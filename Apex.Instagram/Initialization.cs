using MessagePack;

using Utf8Json;

using JsonResolver = Apex.Instagram.Response.Serializer;
using MessagePackResolver = Apex.Instagram.Storage.Serializer.MessagePackFormatter;

namespace Apex.Instagram
{
    internal static class Initialization
    {
        private static bool _done;

        private static readonly object Lock = new object();

        public static void Initialize()
        {
            lock (Lock)
            {
                if ( _done )
                {
                    return;
                }

                _done = true;
            }

            JsonSerializer.SetDefaultResolver(JsonResolver.CustomCompositeResolver.Instance);
            MessagePackSerializer.SetDefaultResolver(MessagePackResolver.CustomCompositeResolver.Instance);
        }
    }
}