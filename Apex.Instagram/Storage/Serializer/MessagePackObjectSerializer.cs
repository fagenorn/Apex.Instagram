using System.IO;
using System.Threading.Tasks;

using Apex.Instagram.Storage.Serializer.MessagePackFormatter;

using MessagePack;

namespace Apex.Instagram.Storage.Serializer
{
    internal class MessagePackObjectSerializer : IObjectSerializer
    {
        public async Task<Stream> SerializeAsync<T>(T data)
        {
            var stream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync(stream, data, CustomCompositeResolver.Instance).ConfigureAwait(false);

            return stream;
        }

        public async Task<T> DeserializeAsync<T>(Stream stream) { return await MessagePackSerializer.DeserializeAsync<T>(stream, CustomCompositeResolver.Instance).ConfigureAwait(false); }
    }
}