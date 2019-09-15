using System;
using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Apex.Instagram.API.Response.Serializer
{
    internal class StringToUlongConverter : JsonConverter<ulong?>
    {
        public override ulong? Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if ( reader.TokenType == JsonTokenType.String )
            {
                var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                if ( Utf8Parser.TryParse(span, out ulong number, out var bytesConsumed) && span.Length == bytesConsumed )
                {
                    return number;
                }

                if ( ulong.TryParse(reader.GetString(), out number) )
                {
                    return number;
                }
            }

            return reader.GetUInt64();
        }

        public override void Write(Utf8JsonWriter writer, ulong? value, JsonSerializerOptions options) { writer.WriteStringValue(value.ToString()); }
    }
}