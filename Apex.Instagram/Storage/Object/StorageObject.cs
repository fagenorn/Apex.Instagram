﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.Storage.Serializer;

namespace Apex.Instagram.Storage.Object
{
    internal class StorageObject<T> : IDisposable
    {
        internal StorageObject(StorageKey key, IStorage storage, int id)
        {
            _key     = (int) key;
            _storage = storage;
            _id      = id;

            _serializer = new MessagePackObjectSerializer();
            _lock       = new SemaphoreSlim(1);
        }

        public void Dispose()
        {
            _lock?.Dispose();
        }

        public async Task<T> LoadAsync()
        {
            await _lock.WaitAsync();
            try
            {
                using (var stream = _storage.Load(_id, _key))
                {
                    if ( stream == null )
                    {
                        return default(T);
                    }

                    return await _serializer.DeserializeAsync<T>(stream);
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task SaveAsync(T data)
        {
            await _lock.WaitAsync();
            try
            {
                using (var stream = await _serializer.SerializeAsync(data))
                {
                    await _storage.SaveAsync(_id, _key, stream);
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        #region Fields

        private readonly int _id;

        private readonly int _key;

        private readonly SemaphoreSlim _lock;

        private readonly IObjectSerializer _serializer;

        private readonly IStorage _storage;

        #endregion
    }
}