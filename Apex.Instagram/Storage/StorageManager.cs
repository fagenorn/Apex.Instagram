using System;
using System.Threading;

using Apex.Instagram.Login;
using Apex.Instagram.Login.Challenge;
using Apex.Instagram.Model.Account;
using Apex.Instagram.Model.Request;
using Apex.Instagram.Storage.Object;

namespace Apex.Instagram.Storage
{
    internal class StorageManager : IDisposable
    {
        public StorageManager(IStorage storage, int id, CancellationToken ct)
        {
            Proxy         = new StorageObject<Proxy>(StorageKey.Proxy, storage, id, ct);
            Cookie        = new StorageObject<CookieCollectionConverter>(StorageKey.Cookie, storage, id, ct);
            AccountInfo   = new StorageObject<AccountInfo>(StorageKey.AccountInfo, storage, id, ct);
            LoginInfo     = new StorageObject<LoginInfo>(StorageKey.LoginInfo, storage, id, ct);
            ChallengeInfo = new StorageObject<ChallengeInfo>(StorageKey.ChallengeInfo, storage, id, ct);
        }

        public StorageObject<Proxy> Proxy { get; }

        public StorageObject<CookieCollectionConverter> Cookie { get; }

        public StorageObject<AccountInfo> AccountInfo { get; }

        public StorageObject<LoginInfo> LoginInfo { get; }

        public StorageObject<ChallengeInfo> ChallengeInfo { get; }

        public void Dispose()
        {
            Proxy?.Dispose();
            Cookie?.Dispose();
            AccountInfo?.Dispose();
            LoginInfo?.Dispose();
            ChallengeInfo?.Dispose();
        }
    }
}