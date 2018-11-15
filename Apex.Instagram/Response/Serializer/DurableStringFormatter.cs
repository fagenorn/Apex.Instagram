using System.Globalization;

using Utf8Json;
using Utf8Json.Resolvers;

namespace Apex.Instagram.Response.Serializer
{
    internal class DurableStringFormatter : IJsonFormatter<string>
    {
        public string Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var token = reader.GetCurrentJsonToken();
            switch ( token )
            {
                case JsonToken.Number:

                    return reader.ReadDouble()
                                 .ToString(CultureInfo.InvariantCulture);
                case JsonToken.String:
                case JsonToken.Null:

                    return reader.ReadString();
                case JsonToken.True:
                case JsonToken.False:

                    return reader.ReadBoolean()
                                 .ToString();
                default:

                    return BuiltinResolver.Instance.GetFormatterWithVerify<string>()
                                          .Deserialize(ref reader, formatterResolver);
            }
        }

        public void Serialize(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver)
        {
            BuiltinResolver.Instance.GetFormatterWithVerify<string>()
                           .Serialize(ref writer, value, formatterResolver);
        }

        #region Singleton     

        private static DurableStringFormatter _instance;

        private static readonly object Lock = new object();

        private DurableStringFormatter() { }

        public static DurableStringFormatter Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new DurableStringFormatter());
                }
            }
        }

        #endregion
    }
}