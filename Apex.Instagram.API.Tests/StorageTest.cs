using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Apex.Instagram.API.Storage.Object;
using Apex.Instagram.API.Tests.Extra;
using Apex.Shared.Model;

using Xunit;

namespace Apex.Instagram.API.Tests
{
    public class StorageTest : IDisposable
    {
        public void Dispose() { _fileStorage.ClearSave(); }

        private readonly FileStorage _fileStorage = new FileStorage(nameof(StorageTest));

        [Fact]
        public async Task Create_Save_Load_Cookies()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .BuildAsync();

            var cookieContainer = new HashSet<Cookie>
                                  {
                                      new Cookie("bb", "ba", "/", "domain.com"),
                                      new Cookie("aa", "ac", "/", "domain.com")
                                  };

            var result = await account.Storage.Cookie.LoadAsync();
            Assert.Null(result);

            await account.Storage.Cookie.SaveAsync(new CookieCollectionConverter(cookieContainer));
            result = await account.Storage.Cookie.LoadAsync();
            Assert.Equal(2, result.Cookies.Count);
        }

        [Fact]
        public async Task Create_Save_Load_Proxy()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .BuildAsync();

            Assert.Equal(string.Empty, account.GetProxy());
            await account.UpdateProxyAsync(new Proxy("https://111.222.333.444:1234/"));
            Assert.Equal("https://111.222.333.444:1234/", account.GetProxy());

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .BuildAsync();

            Assert.Equal("https://111.222.333.444:1234/", account.GetProxy());
            await account.UpdateProxyAsync(new Proxy("http://444.333.222.111:4321/"));
            Assert.Equal("http://444.333.222.111:4321/", account.GetProxy());
        }

        [Fact]
        public async Task Load_Store_Epoch()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .BuildAsync();

            Assert.Equal(0, account.LoginClient.LoginInfo.LastLogin.Last.Value);
            account.LoginClient.LoginInfo.LastLogin.Update();
            Assert.NotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last.Value);
            account.LoginClient.LoginInfo.LastLogin = new LastAction(TimeSpan.FromDays(1), new Epoch(565));
            Assert.Equal(565, account.LoginClient.LoginInfo.LastLogin.Last.Value);
            await account.Storage.LoginInfo.SaveAsync(account.LoginClient.LoginInfo);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .BuildAsync();

            Assert.Equal(565, account.LoginClient.LoginInfo.LastLogin.Last.Value);
        }

        [Fact]
        public async Task Update_Password()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetPassword("test_password123")
                                                    .BuildAsync();

            var storageInfo = await account.Storage.AccountInfo.LoadAsync();
            Assert.Equal("test_password123", account.AccountInfo.Password);
            Assert.Equal("test_password123", storageInfo.Password);

            await account.UpdatePasswordAsync("new_password321");

            storageInfo = await account.Storage.AccountInfo.LoadAsync();
            Assert.Equal("new_password321", account.AccountInfo.Password);
            Assert.Equal("new_password321", storageInfo.Password);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .BuildAsync();

            Assert.Equal("new_password321", account.AccountInfo.Password);
        }

        [Fact]
        public async Task Update_Username()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetUsername("test_username123")
                                                    .BuildAsync();

            var storageInfo = await account.Storage.AccountInfo.LoadAsync();
            Assert.Equal("test_username123", account.AccountInfo.Username);
            Assert.Equal("test_username123", storageInfo.Username);

            await account.UpdateUsernameAsync("new_username321");

            storageInfo = await account.Storage.AccountInfo.LoadAsync();
            Assert.Equal("new_username321", account.AccountInfo.Username);
            Assert.Equal("new_username321", storageInfo.Username);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .BuildAsync();

            Assert.Equal("new_username321", account.AccountInfo.Username);
        }
    }
}