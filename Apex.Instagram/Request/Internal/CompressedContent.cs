using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Apex.Instagram.Request.Internal
{
    internal enum CompressionType
    {
        Gzip,

        Deflate
    }

    internal class CompressedContent : HttpContent
    {
        private readonly CompressionType _compressionType;

        private readonly HttpContent _originalContent;

        public CompressedContent(HttpContent content, CompressionType encodingType)
        {
            _originalContent = content ?? throw new ArgumentNullException(nameof(content));
            _compressionType = encodingType;

            foreach ( var header in _originalContent.Headers )
            {
                Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            Headers.ContentEncoding.Add(encodingType.ToString()
                                                    .ToLowerInvariant());
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;

            return false;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Stream compressedStream;

            switch ( _compressionType )
            {
                case CompressionType.Gzip:
                    compressedStream = new GZipStream(stream, CompressionMode.Compress, true);

                    break;
                case CompressionType.Deflate:
                    compressedStream = new DeflateStream(stream, CompressionMode.Compress, true);

                    break;
                default:

                    throw new ArgumentOutOfRangeException();
            }

            return _originalContent.CopyToAsync(compressedStream)
                                   .ContinueWith(tsk => { compressedStream?.Dispose(); });
        }
    }
}