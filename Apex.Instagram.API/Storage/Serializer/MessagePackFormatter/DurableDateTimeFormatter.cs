using System;
using System.Globalization;

using MessagePack;
using MessagePack.Formatters;

namespace Apex.Instagram.API.Storage.Serializer.MessagePackFormatter
{
    internal class DurableDateTimeFormatter : IMessagePackFormatter<DateTime>
    {
        public DateTime Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            if ( MessagePackBinary.GetMessagePackType(bytes, offset) == MessagePackType.String )
            {
                var str = MessagePackBinary.ReadString(bytes, offset, out readSize);

                return DateTime.ParseExact(str, @"yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            }

            return MessagePackBinary.ReadDateTime(bytes, offset, out readSize);
        }

        public int Serialize(ref byte[] bytes, int offset, DateTime value, IFormatterResolver formatterResolver) { return MessagePackBinary.WriteDateTime(ref bytes, offset, value); }

        #region Singleton     

        private static DurableDateTimeFormatter _instance;

        private static readonly object Lock = new object();

        private DurableDateTimeFormatter() { }

        public static DurableDateTimeFormatter Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new DurableDateTimeFormatter());
                }
            }
        }

        #endregion
    }
}