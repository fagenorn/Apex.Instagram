using Apex.Instagram.API.Response.Serializer;

using MessagePack;

using Utf8Json;

namespace Apex.Instagram.API
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

            JsonSerializer.SetDefaultResolver(CustomCompositeResolver.Instance);
            MessagePackSerializer.SetDefaultResolver(Storage.Serializer.MessagePackFormatter.CustomCompositeResolver.Instance);
        }
    }
}