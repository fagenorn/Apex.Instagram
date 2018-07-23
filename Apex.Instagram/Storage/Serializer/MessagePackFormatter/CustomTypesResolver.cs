using System;
using System.Collections.Generic;
using System.Net;

using MessagePack;
using MessagePack.Formatters;

namespace Apex.Instagram.Storage.Serializer.MessagePackFormatter
{
    internal class CustomTypesResolver : IFormatterResolver
    {
        // Resolver should be singleton.
        public static IFormatterResolver Instance = new CustomTypesResolver();

        private CustomTypesResolver() { }

        public IMessagePackFormatter<T> GetFormatter<T>() { return FormatterCache<T>.Formatter; }

        private static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> Formatter;

            static FormatterCache() { Formatter = (IMessagePackFormatter<T>) CustomResolverGetFormatterHelper.GetFormatter(typeof(T)); }
        }

        internal static class CustomResolverGetFormatterHelper
        {
            // If type is concrete type, use type-formatter map
            private static readonly Dictionary<Type, object> FormatterMap = new Dictionary<Type, object>
                                                                            {
                                                                                {
                                                                                    typeof(Cookie), CookieFormatter.Instance
                                                                                },
                                                                                {
                                                                                    typeof(DateTime), DurableDateTimeFormatter.Instance
                                                                                }
                                                                            };

            internal static object GetFormatter(Type t)
            {
                if ( FormatterMap.TryGetValue(t, out var formatter) )
                {
                    return formatter;
                }

                // If type can not get, must return null for fallback mecanism.
                return null;
            }
        }
    }
}