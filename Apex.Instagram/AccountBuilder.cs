using System.Threading.Tasks;

using Apex.Instagram.Exception;
using Apex.Instagram.Logger;
using Apex.Instagram.Storage;

namespace Apex.Instagram
{
    public class AccountBuilder
    {
        private int? _id;

        private IApexLogger _logger;

        private IStorage _storage;

        public AccountBuilder SetId(int id)
        {
            _id = id;

            return this;
        }

        public AccountBuilder SetStorage(IStorage storage)
        {
            _storage = storage;

            return this;
        }

        public AccountBuilder SetLogger(IApexLogger logger)
        {
            _logger = logger;

            return this;
        }

        public async Task<Account> BuildAsync()
        {
            if ( _id == null )
            {
                throw new AccountBuilderException("You must set an id for the account.");
            }

            if ( _storage == null )
            {
                throw new AccountBuilderException("You must set a storage interface.");
            }

            var account = await Account.CreateAsync(new StorageManager(_storage, (int) _id), _logger);

            return account;
        }
    }
}