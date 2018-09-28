using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.Storage;

namespace Apex.Instagram.Tests
{
    internal class FileStorage : IStorage
    {
        public async Task SaveAsync(int id, int subId, Stream data, CancellationToken ct = default)
        {
            Directory.CreateDirectory("tests");
            using (var fs = new FileStream($"tests/{id}.{subId}.txt", FileMode.Create, FileAccess.Write))
            {
                data.Seek(0, SeekOrigin.Begin);
                await data.CopyToAsync(fs, 4096, ct);
            }
        }

        public Stream Load(int id, int subId)
        {
            Directory.CreateDirectory("tests");
            try
            {
                var fs = new FileStream($"tests/{id}.{subId}.txt", FileMode.Open, FileAccess.Read);

                return fs;
            }
            catch
            {
                return null;
            }
        }
    }
}