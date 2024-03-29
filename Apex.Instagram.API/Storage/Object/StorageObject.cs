﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Apex.Instagram.API.Storage.Serializer;

namespace Apex.Instagram.API.Storage.Object
{
    internal class StorageObject<T> : IDisposable
    {
        internal StorageObject(StorageKey key, IStorage storage, int id, CancellationToken cancellationToken)
        {
            _key               = (int) key;
            _storage           = storage;
            _id                = id;
            _cancellationToken = cancellationToken;

            _serializer = new MessagePackObjectSerializer();
            _lock       = new SemaphoreSlim(1);
        }

        public void Dispose()
        {
            _disposed = true;
            _lock?.Dispose();
        }

        public async Task<T> LoadAsync()
        {
            if ( _disposed )
            {
                throw new ObjectDisposedException(nameof(StorageObject<T>));
            }

            await _lock.WaitAsync(_cancellationToken)
                       .ConfigureAwait(false);

            try
            {
                using (var stream = _storage.Load(_id, _key))
                {
                    if ( stream == null )
                    {
                        return default;
                    }

                    return await _serializer.DeserializeAsync<T>(stream)
                                            .ConfigureAwait(false);
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task SaveAsync(T data)
        {
            if ( _disposed )
            {
                throw new ObjectDisposedException(nameof(StorageObject<T>));
            }

            await _lock.WaitAsync(_cancellationToken)
                       .ConfigureAwait(false);

            try
            {
                using (var stream = await _serializer.SerializeAsync(data)
                                                     .ConfigureAwait(false))
                {
                    await _storage.SaveAsync(_id, _key, stream, _cancellationToken)
                                  .ConfigureAwait(false);
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

        private readonly CancellationToken _cancellationToken;

        private readonly IObjectSerializer _serializer;

        private readonly IStorage _storage;

        private bool _disposed;

        #endregion
    }
}