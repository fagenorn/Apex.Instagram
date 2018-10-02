using Utf8Json;

namespace Apex.Instagram.Response.JsonFormatter
{
    public class DurableUlongFormatter : IJsonFormatter<ulong?>
    {
        public void Serialize(ref JsonWriter writer, ulong? value, IJsonFormatterResolver formatterResolver)
        {
            formatterResolver.GetFormatterWithVerify<ulong?>()
                             .Serialize(ref writer, value, formatterResolver);
        }

        public ulong? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var token = reader.GetCurrentJsonToken();
            switch ( token )
            {
                case JsonToken.String:

                    return ulong.Parse(reader.ReadString());
                default:

                    return formatterResolver.GetFormatterWithVerify<ulong?>()
                                            .Deserialize(ref reader, formatterResolver);
            }
        }
    }
}