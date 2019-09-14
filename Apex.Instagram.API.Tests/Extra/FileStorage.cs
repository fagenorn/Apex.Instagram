using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.API.Storage;

namespace Apex.Instagram.API.Tests.Extra
{
    internal class FileStorage : IStorage
    {
        private readonly string _folder;

        public FileStorage(string folder) { _folder = folder; }

        public async Task SaveAsync(int id, int subId, Stream data, CancellationToken ct = default)
        {
            Directory.CreateDirectory(_folder);
            await using var fs = new FileStream($"{_folder}/{id}.{subId}.txt", FileMode.Create, FileAccess.Write);
            data.Seek(0, SeekOrigin.Begin);
            await data.CopyToAsync(fs, 4096, ct);
        }

        public Stream Load(int id, int subId)
        {
            Directory.CreateDirectory(_folder);
            try
            {
                var fs = new FileStream($"{_folder}/{id}.{subId}.txt", FileMode.Open, FileAccess.Read);

                return fs;
            }
            catch
            {
                return null;
            }
        }

        public void ClearSave()
        {
            if ( Directory.Exists(_folder) )
            {
                var files = Directory.GetFiles(_folder);
                foreach ( var file in files )
                {
                    File.Delete(file);
                }
            }
        }
    }
}