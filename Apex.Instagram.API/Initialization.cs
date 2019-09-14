using System.Runtime.CompilerServices;

using Apex.Instagram.API.Response.Serializer;

using MessagePack;

using Utf8Json;

[assembly: InternalsVisibleTo("Apex.Instagram.API.Tests")]

[assembly: InternalsVisibleTo("MessagePack")]
[assembly: InternalsVisibleTo("MessagePack.Resolvers.DynamicObjectResolver")]
[assembly: InternalsVisibleTo("MessagePack.Resolvers.DynamicUnionResolver")]

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