using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.API.Storage;

namespace Apex.Instagram.Service.Database
{
    public class ApiMemoryStorage : IStorage
    {
        private readonly IDictionary<int, IDictionary<int, Stream>> _storage = new Dictionary<int, IDictionary<int, Stream>>();

        public Task SaveAsync(int id, int subId, Stream data, CancellationToken ct = default)
        {
            if ( !_storage.ContainsKey(id) )
            {
                _storage[id] = new Dictionary<int, Stream>();
            }

            _storage[id][subId] = data;

            return Task.CompletedTask;
        }

        public Stream Load(int id, int subId)
        {
            if ( !_storage.ContainsKey(id) || !_storage[id]
                     .ContainsKey(subId) )
            {
                return null;
            }

            return _storage[id][subId];
        }
    }
}