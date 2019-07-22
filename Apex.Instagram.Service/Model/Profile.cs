using System.Threading.Tasks;

using Apex.Instagram.API;
using Apex.Instagram.API.Storage;

namespace Apex.Instagram.Service.Model
{
    public class Profile : IModel
    {
        #region Fields

        private Account _account;

        #endregion

        internal Profile() { }

        internal async Task InitializeAsync(IStorage storage, string username = null, string password = null)
        {
            _account = await new AccountBuilder().SetId(Id)
                                                 .SetStorage(storage)
                                                 .SetUsername(username)
                                                 .SetPassword(password)
                                                 .BuildAsync();
        }

        #region Public Methods

        public async Task UpdateUsernameAsync(string username) { await _account.UpdateUsernameAsync(username); }

        public async Task UpdatePasswordAsync(string password) { await _account.UpdatePasswordAsync(password); }

        #endregion

        #region Properties

        public string Username => _account.AccountInfo.Username;

        public string Password => _account.AccountInfo.Password;

        public int Id { get; set; }

        #endregion
    }
}