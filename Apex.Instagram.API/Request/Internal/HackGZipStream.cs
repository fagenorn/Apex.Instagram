using System.IO;
using System.IO.Compression;


namespace Apex.Instagram.API.Request.Internal
{
    internal class HackGZipStream : GZipStream
    {
        public HackGZipStream(Stream stream, CompressionMode mode) : base(stream, mode)
        {

        }

        public HackGZipStream(Stream stream, CompressionMode mode, bool leaveOpen) : base(stream, mode, leaveOpen)
        {

        }

        public HackGZipStream(Stream stream, CompressionLevel level) : base(stream, level)
        {

        }

        public HackGZipStream(Stream stream, CompressionLevel level, bool leaveOpen) : base(stream, level, leaveOpen)
        {

        }
    }
}
