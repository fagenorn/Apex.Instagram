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
                    var value = reader.ReadString();

                    if ( string.IsNullOrWhiteSpace(value) )
                    {
                        return 0;
                    }

                    return ulong.TryParse(value, out var result) ? result : 0;
                default:

                    return formatterResolver.GetFormatterWithVerify<ulong?>()
                                            .Deserialize(ref reader, formatterResolver);
            }
        }
    }
}