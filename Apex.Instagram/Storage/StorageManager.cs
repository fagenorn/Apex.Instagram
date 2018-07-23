using System;

using Apex.Instagram.Model.Request;
using Apex.Instagram.Storage.Object;

namespace Apex.Instagram.Storage
{
    internal class StorageManager : IDisposable
    {
        public StorageManager(IStorage storage, int id)
        {
            Proxy  = new StorageObject<Proxy>(StorageKey.Proxy, storage, id);
            Cookie = new StorageObject<CookieCollectionConverter>(StorageKey.Cookie, storage, id);
        }

        public StorageObject<Proxy> Proxy { get; }

        public StorageObject<CookieCollectionConverter> Cookie { get; }

        public void Dispose()
        {
            Proxy?.Dispose();
            Cookie?.Dispose();
        }
    }
}