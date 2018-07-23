using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;

using MessagePack;
using MessagePack.Formatters;

namespace Apex.Instagram.Storage.Serializer.MessagePackFormatter
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class CookieFormatter : IMessagePackFormatter<Cookie>
    {
        public int Serialize(ref byte[] bytes, int offset, Cookie value, IFormatterResolver formatterResolver)
        {
            if ( value == null )
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }

            var type = typeof(Cookie);

            var startOffset = offset;
            offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 14);

            var m_name = type.GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteString(ref bytes, offset, (string) m_name.GetValue(value));

            var m_value = type.GetField("m_value", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteString(ref bytes, offset, (string) m_value.GetValue(value));

            var m_port_implicit = type.GetField("m_port_implicit", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, (bool) m_port_implicit.GetValue(value));

            var m_port = type.GetField("m_port", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteString(ref bytes, offset, (string) m_port.GetValue(value));

            var m_path_implicit = type.GetField("m_path_implicit", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, (bool) m_path_implicit.GetValue(value));

            var m_path = type.GetField("m_path", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteString(ref bytes, offset, (string) m_path.GetValue(value));

            var m_domain = type.GetField("m_domain", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteString(ref bytes, offset, (string) m_domain.GetValue(value));

            var m_domain_implicit = type.GetField("m_domain_implicit", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, (bool) m_domain_implicit.GetValue(value));

            var m_timeStamp = type.GetField("m_timeStamp", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteDateTime(ref bytes, offset, (DateTime) m_timeStamp.GetValue(value));

            var m_httpOnly = type.GetField("m_httpOnly", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, (bool) m_httpOnly.GetValue(value));

            var m_discard = type.GetField("m_discard", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, (bool) m_discard.GetValue(value));

            var m_expires = type.GetField("m_expires", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteDateTime(ref bytes, offset, (DateTime) m_expires.GetValue(value));

            var m_version = type.GetField("m_version", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, (int) m_version.GetValue(value));

            var m_secure = type.GetField("m_secure", BindingFlags.NonPublic | BindingFlags.Instance);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, (bool) m_secure.GetValue(value));

            return offset - startOffset;
        }

        public Cookie Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            if ( MessagePackBinary.IsNil(bytes, offset) )
            {
                readSize = 1;

                return null;
            }

            var startOffset = offset;
            var count       = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            if ( count != 14 )
            {
                throw new InvalidOperationException("Invalid Cookie format.");
            }

            var m_name = MessagePackBinary.ReadString(bytes, offset, out readSize);
            offset += readSize;

            if ( m_name == null )
            {
                readSize = 1;

                return null;
            }

            var m_value = MessagePackBinary.ReadString(bytes, offset, out readSize);
            offset += readSize;

            var m_port_implicit = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
            offset += readSize;

            var m_port = MessagePackBinary.ReadString(bytes, offset, out readSize);
            offset += readSize;

            var m_path_implicit = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
            offset += readSize;

            var m_path = MessagePackBinary.ReadString(bytes, offset, out readSize);
            offset += readSize;

            var m_domain = MessagePackBinary.ReadString(bytes, offset, out readSize);
            offset += readSize;

            var m_domain_implicit = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
            offset += readSize;

            var m_timeStamp = DurableDateTimeFormatter.Instance.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;

            var m_httpOnly = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
            offset += readSize;

            var m_discard = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
            offset += readSize;

            var m_expires = DurableDateTimeFormatter.Instance.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;

            var m_version = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            var m_secure = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
            offset += readSize;

            readSize = offset - startOffset;

            var cookie = new Cookie(m_name, m_value);
            var type   = cookie.GetType();

            if ( !m_port_implicit )
            {
                cookie.Port = m_port;
            }

            if ( !m_path_implicit )
            {
                cookie.Path = m_path;
            }

            cookie.Domain = m_domain;

            var domainImplicit = type.GetProperty("DomainImplicit", BindingFlags.NonPublic | BindingFlags.Instance);
            domainImplicit.SetValue(cookie, m_domain_implicit, null);

            var m_timeStamp_field = type.GetField("m_timeStamp", BindingFlags.NonPublic | BindingFlags.Instance);
            m_timeStamp_field.SetValue(cookie, m_timeStamp);

            cookie.HttpOnly = m_httpOnly;
            cookie.Discard  = m_discard;
            cookie.Expires  = m_expires;
            cookie.Version  = m_version;
            cookie.Secure   = m_secure;

            return cookie;
        }

        #region Singleton     

        private static CookieFormatter _instance;

        private static readonly object Lock = new object();

        private CookieFormatter() { }

        public static CookieFormatter Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new CookieFormatter());
                }
            }
        }

        #endregion
    }
}