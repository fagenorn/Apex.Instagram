using System.IO;
using System.Threading.Tasks;

namespace Apex.Instagram.API.Storage.Serializer
{
    internal interface IObjectSerializer
    {
        Task<Stream> SerializeAsync<T>(T data);

        Task<T> DeserializeAsync<T>(Stream stream);
    }
}