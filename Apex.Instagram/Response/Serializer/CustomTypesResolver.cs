using System;
using System.Collections.Generic;

using Utf8Json;

namespace Apex.Instagram.Response.Serializer
{
    internal class CustomTypesResolver : IJsonFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly IJsonFormatterResolver Instance = new CustomTypesResolver();

        private CustomTypesResolver() { }

        public IJsonFormatter<T> GetFormatter<T>() { return FormatterCache<T>.Formatter; }

        private static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> Formatter;

            static FormatterCache() { Formatter = (IJsonFormatter<T>) CustomResolverGetFormatterHelper.GetFormatter(typeof(T)); }
        }

        internal static class CustomResolverGetFormatterHelper
        {
            // If type is concrete type, use type-formatter map
            private static readonly Dictionary<Type, object> FormatterMap = new Dictionary<Type, object>
                                                                            {
                                                                                {typeof(ulong?), DurableUlongFormatter.Instance}
                                                                            };

            internal static object GetFormatter(Type t)
            {
                if ( FormatterMap.TryGetValue(t, out var formatter) )
                {
                    return formatter;
                }

                return null;
            }
        }
    }
}