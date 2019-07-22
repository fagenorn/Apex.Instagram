using System.Threading.Tasks;

using Apex.Instagram.API.Storage;
using Apex.Instagram.Service.Database;
using Apex.Instagram.Service.Model;

namespace Apex.Instagram.Service.Service
{
    public class AccountService
    {
        private readonly IStorage _apiStorage = new ApiMemoryStorage();

        private readonly AccountDatabase _storage = new AccountDatabase();

        public async Task<Profile> CreateAsync(string username, string password)
        {
            var profile = new Profile();

            await _storage.SaveAsync(profile);
            await profile.InitializeAsync(_apiStorage, username, password);

            return profile;
        }

        public async Task<Profile> GetAsync(int id) { return await _storage.LoadAsync(id); }
    }
}