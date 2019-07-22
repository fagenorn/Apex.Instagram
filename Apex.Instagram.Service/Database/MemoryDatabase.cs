using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Apex.Instagram.Service.Model;

namespace Apex.Instagram.Service.Database
{
    internal abstract class MemoryDatabase<T> : IDatabase<T> where T : IModel
    {
        private readonly IDictionary<int, T> _storage = new Dictionary<int, T>();

        private int _id;

        public Task DeleteAsync(int id)
        {
            _storage.Remove(id);

            return Task.CompletedTask;
        }

        public Task<List<T>> LoadAllAsync() { return Task.FromResult(_storage.Values.ToList()); }

        public Task<T> LoadAsync(int id) { return Task.FromResult(_storage[id]); }

        public Task SaveAsync(T data)
        {
            data.Id = GenerateUniqueId();

            _storage[data.Id] = data;

            return Task.CompletedTask;
        }

        private int GenerateUniqueId() { return _id++; }
    }
}