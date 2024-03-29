﻿using System.IO;
using System.Threading.Tasks;

using MessagePack;

namespace Apex.Instagram.API.Storage.Serializer
{
    internal class MessagePackObjectSerializerJson : IObjectSerializer
    {
        public async Task<Stream> SerializeAsync<T>(T data)
        {
            var stream       = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            var json         = MessagePackSerializer.ToJson(data);
            await streamWriter.WriteAsync(json)
                              .ConfigureAwait(false);

            await streamWriter.FlushAsync()
                              .ConfigureAwait(false);

            return stream;
        }

        public async Task<T> DeserializeAsync<T>(Stream stream)
        {
            using (var textReader = new StreamReader(stream))
            {
                using (var memoryStream = new MemoryStream(MessagePackSerializer.FromJson(textReader)))
                {
                    return await MessagePackSerializer.DeserializeAsync<T>(memoryStream)
                                                      .ConfigureAwait(false);
                }
            }
        }
    }
}