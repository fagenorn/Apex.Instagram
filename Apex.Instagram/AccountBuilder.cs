using System.Threading.Tasks;

using Apex.Instagram.Exception;
using Apex.Instagram.Logger;
using Apex.Instagram.Model.Request;
using Apex.Instagram.Storage;

namespace Apex.Instagram
{
    public class AccountBuilder
    {
        private int? _id;

        private IApexLogger _logger;

        private string _password;

        private Proxy _proxy;

        private IStorage _storage;

        private string _username;

        public AccountBuilder SetId(int id)
        {
            _id = id;

            return this;
        }

        public AccountBuilder SetUsername(string username)
        {
            _username = username;

            return this;
        }

        public AccountBuilder SetPassword(string password)
        {
            _password = password;

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

        public AccountBuilder SetProxy(Proxy proxy)
        {
            _proxy = proxy;

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

            if ( _proxy != null )
            {
                await account.UpdateProxy(_proxy);
            }

            if ( !string.IsNullOrWhiteSpace(_username) )
            {
                await account.UpdateUsername(_username);
            }

            if ( !string.IsNullOrWhiteSpace(_password) )
            {
                await account.UpdatePassword(_password);
            }

            return account;
        }
    }
}