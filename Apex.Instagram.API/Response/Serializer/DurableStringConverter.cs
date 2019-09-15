using System;
using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Apex.Instagram.API.Response.Serializer
{
    internal class DurableStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if ( reader.TokenType == JsonTokenType.Number )
            {
                var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

                return Encoding.UTF8.GetString(span);
            }

            if ( reader.TokenType == JsonTokenType.True )
            {
                return "True";
            }

            if ( reader.TokenType == JsonTokenType.False )
            {
                return "False";
            }

            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if ( ulong.TryParse(value, out var _ulong) )
            {
                writer.WriteNumberValue(_ulong);

                return;
            }

            if ( bool.TryParse(value, out var _bool) )
            {
                writer.WriteBooleanValue(_bool);

                return;
            }

            writer.WriteStringValue(value);
        }
    }
}