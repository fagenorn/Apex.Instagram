using System.Threading.Tasks;

using Apex.Instagram.Service.Database;
using Apex.Instagram.Service.Model;
using Apex.Instagram.Service.Service;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apex.Instagram.Service.Tests
{
    [TestClass]
    public class StorageTests
    {
        [TestMethod]
        public async Task Adding_And_Removing_Account_From_Database()
        {
            var database = new AccountDatabase();

            var account1 = new Profile();
            var account2 = new Profile();

            var results = await database.LoadAllAsync();
            Assert.AreEqual(0, results.Count);

            Assert.AreEqual(0, account1.Id);
            await database.SaveAsync(account1);
            results = await database.LoadAllAsync();
            Assert.AreEqual(1, results.Count);

            Assert.AreEqual(0, account2.Id);
            await database.SaveAsync(account2);
            Assert.AreNotEqual(0, account2.Id);
            results = await database.LoadAllAsync();
            Assert.AreEqual(2, results.Count);

            await database.DeleteAsync(account2.Id);
            results = await database.LoadAllAsync();
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(account1, results[0]);
            Assert.AreNotEqual(account2, results[0]);
        }

        [TestMethod]
        public async Task Api_Storage_Loading_And_Saving()
        {
            var service = new AccountService();
            var profile = await service.CreateAsync("test", "best");
            Assert.AreEqual("test", profile.Username);
            Assert.AreEqual("best", profile.Password);
        }
    }
}