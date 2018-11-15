using System.Globalization;

using Utf8Json;

namespace Apex.Instagram.Response.Serializer
{
    public class DurableStringFormatter : IJsonFormatter<string>
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

                    throw new JsonParsingException($"Invalid JSON token. Token: {token}", reader.GetBufferUnsafe(), reader.GetCurrentOffsetUnsafe(), reader.GetCurrentOffsetUnsafe(), string.Empty);
            }
        }

        public void Serialize(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver) { writer.WriteString(value); }
    }
}