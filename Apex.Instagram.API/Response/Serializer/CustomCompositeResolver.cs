using Utf8Json;
using Utf8Json.ImmutableCollection;
using Utf8Json.Resolvers;

namespace Apex.Instagram.API.Response.Serializer
{
    internal class CustomCompositeResolver : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new CustomCompositeResolver();

        private static readonly IJsonFormatterResolver[] Resolvers =
        {
            CustomTypesResolver.Instance,
            ImmutableCollectionResolver.Instance,
            StandardResolver.Default
        };

        private CustomCompositeResolver() { }

        public IJsonFormatter<T> GetFormatter<T>() { return FormatterCache<T>.Formatter; }

        private static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> Formatter;

            static FormatterCache()
            {
                foreach ( var item in Resolvers )
                {
                    var f = item.GetFormatter<T>();
                    if ( f != null )
                    {
                        Formatter = f;

                        return;
                    }
                }
            }
        }
    }
}