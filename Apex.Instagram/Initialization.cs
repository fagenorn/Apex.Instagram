using MessagePack;

using Utf8Json;
using Utf8Json.Formatters;
using Utf8Json.Resolvers;

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

            var customResolver = CompositeResolver.Create(new IJsonFormatter[] {new DynamicObjectTypeFallbackFormatter(JsonResolver.CustomCompositeResolver.Instance)}, new[] {JsonResolver.CustomCompositeResolver.Instance});

            JsonSerializer.SetDefaultResolver(customResolver);
            MessagePackSerializer.SetDefaultResolver(MessagePackResolver.CustomCompositeResolver.Instance);

            _done = true;
        }
    }
}