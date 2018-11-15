using Utf8Json;
using Utf8Json.Resolvers;

namespace Apex.Instagram.Response.Serializer
{
    public class DurableUlongFormatter : IJsonFormatter<ulong?>
    {
        public void Serialize(ref JsonWriter writer, ulong? value, IJsonFormatterResolver formatterResolver)
        {
            BuiltinResolver.Instance.GetFormatterWithVerify<ulong?>()
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

                    return BuiltinResolver.Instance.GetFormatterWithVerify<ulong?>()
                                          .Deserialize(ref reader, formatterResolver);
            }
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