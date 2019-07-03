using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;

namespace Apex.Instagram.API.Storage.Serializer.MessagePackFormatter
{
    internal class CustomCompositeResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new CustomCompositeResolver();

        private static readonly IFormatterResolver[] Resolvers =
        {
            CustomTypesResolver.Instance,
            BuiltinResolver.Instance,
            AttributeFormatterResolver.Instance,
            DynamicEnumResolver.Instance,
            DynamicGenericResolver.Instance,
            DynamicUnionResolver.Instance,
            DynamicObjectResolver.Instance,
            DynamicContractlessObjectResolver.Instance,
            TypelessObjectResolver.Instance
        };

        private CustomCompositeResolver() { }

        public IMessagePackFormatter<T> GetFormatter<T>() { return FormatterCache<T>.Formatter; }

        private static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> Formatter;

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