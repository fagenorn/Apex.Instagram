using System.Threading.Tasks;

using Apex.Instagram.Exception;
using Apex.Instagram.Logger;
using Apex.Instagram.Model.Request;
using Apex.Instagram.Storage;

namespace Apex.Instagram
{
    /// <summary>
    ///     Builder for <see cref="Account" /> type.
    /// </summary>
    public class AccountBuilder
    {
        private bool _disposed;

        private int? _id;

        private IApexLogger _logger;

        private string _password;

        private Proxy _proxy;

        private IStorage _storage;

        private string _username;

        /// <summary>
        ///     Sets the identifier of the account.
        ///     If an account with the same identifier inside the <see cref="IStorage" />, the information will be retrieved
        ///     automatically.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public AccountBuilder SetId(int id)
        {
            _id = id;

            return this;
        }

        /// <summary>Sets the username.</summary>
        /// <param name="username">The account username.</param>
        public AccountBuilder SetUsername(string username)
        {
            _username = username;

            return this;
        }

        /// <summary>
        ///     Sets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        public AccountBuilder SetPassword(string password)
        {
            _password = password;

            return this;
        }

        /// <summary>
        ///     Sets the storage intertface.
        /// </summary>
        /// <param name="storage">The storage.</param>
        public AccountBuilder SetStorage(IStorage storage)
        {
            _storage = storage;

            return this;
        }

        /// <summary>
        ///     Sets the logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public AccountBuilder SetLogger(IApexLogger logger)
        {
            _logger = logger;

            return this;
        }

        /// <summary>
        ///     Sets the account proxy.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        public AccountBuilder SetProxy(Proxy proxy)
        {
            _proxy = proxy;

            return this;
        }

        /// <summary>Builds the account asynchronous.</summary>
        /// <returns>
        ///     <see cref="Account" />
        /// </returns>
        /// <exception cref="AccountBuilderException">Account has already been build.</exception>
        /// <exception cref="AccountBuilderException">You must set an id for the account.</exception>
        /// <exception cref="AccountBuilderException">You must set a storage interface.</exception>
        public async Task<Account> BuildAsync()
        {
            if ( _disposed )
            {
                throw new AccountBuilderException("Account has already been build.");
            }

            if ( _id == null )
            {
                throw new AccountBuilderException("You must set an id for the account.");
            }

            if ( _storage == null )
            {
                throw new AccountBuilderException("You must set a storage interface.");
            }

            var account = await Account.CreateAsync(_storage, _id.Value, _logger)
                                       .ConfigureAwait(false);

            if ( _proxy != null )
            {
                await account.UpdateProxyAsync(_proxy)
                             .ConfigureAwait(false);
            }

            if ( !string.IsNullOrWhiteSpace(_username) )
            {
                await account.UpdateUsernameAsync(_username)
                             .ConfigureAwait(false);
            }

            if ( !string.IsNullOrWhiteSpace(_password) )
            {
                await account.UpdatePasswordAsync(_password)
                             .ConfigureAwait(false);
            }

            _disposed = true;

            return account;
        }
    }
}