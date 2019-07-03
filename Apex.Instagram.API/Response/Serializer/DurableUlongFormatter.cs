using Utf8Json;
using Utf8Json.Formatters;

namespace Apex.Instagram.API.Response.Serializer
{
    public class DurableUlongFormatter : IJsonFormatter<ulong?>
    {
        public void Serialize(ref JsonWriter writer, ulong? value, IJsonFormatterResolver formatterResolver) { NullableUInt64Formatter.Default.Serialize(ref writer, value, formatterResolver); }

        public ulong? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var token = reader.GetCurrentJsonToken();
            switch ( token )
            {
                case JsonToken.String:
                    var value = reader.ReadString();

                    if ( string.IsNullOrWhiteSpace(value) )
                    {
                        return 0;
                    }

                    if ( !IsDigits(value) )
                    {
                        throw new JsonParsingException("String can't be converted to a number.", reader.GetBufferUnsafe(), reader.GetCurrentOffsetUnsafe(), reader.GetCurrentOffsetUnsafe(), value);
                    }

                    return ulong.Parse(value);
                case JsonToken.Number:
                case JsonToken.Null:

                    return NullableUInt64Formatter.Default.Deserialize(ref reader, formatterResolver);
                default:

                    throw new JsonParsingException($"Invalid JSON token. Token: {token}", reader.GetBufferUnsafe(), reader.GetCurrentOffsetUnsafe(), reader.GetCurrentOffsetUnsafe(), string.Empty);
            }
        }

        private bool IsDigits(string s)
        {
            if ( string.IsNullOrEmpty(s) )
            {
                return false;
            }

            for ( var i = 0; i < s.Length; i++ )
            {
                if ( (s[i] ^ '0') > 9 )
                {
                    return false;
                }
            }

            return true;
        }

        #region Singleton     

        private static DurableUlongFormatter _instance;

        private static readonly object Lock = new object();

        private DurableUlongFormatter() { }

        public static DurableUlongFormatter Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new DurableUlongFormatter());
                }
            }
        }

        #endregion
    }
}