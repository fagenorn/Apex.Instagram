using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Apex.Instagram.Model.Request;
using Apex.Instagram.Storage.Object;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apex.Instagram.Tests
{
    /// <summary>
    ///     Summary description for StorageTest
    /// </summary>
    [TestClass]
    public class StorageTest
    {
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public async Task Create_Save_Load_Proxy()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .BuildAsync();

            Assert.AreEqual(string.Empty, account.GetProxy());
            await account.UpdateProxy(new Proxy("https://111.222.333.444:1234/"));
            Assert.AreEqual("https://111.222.333.444:1234/", account.GetProxy());

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(fileStorage)
                                                .BuildAsync();

            Assert.AreEqual("https://111.222.333.444:1234/", account.GetProxy());
            await account.UpdateProxy(new Proxy("http://444.333.222.111:4321/"));
            Assert.AreEqual("http://444.333.222.111:4321/", account.GetProxy());
        }

        [TestMethod]
        public async Task Create_Save_Load_Cookies()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .BuildAsync();

            var cookieContainer = new HashSet<Cookie>
                                  {
                                      new Cookie("bb", "ba", "/", "domain.com"),
                                      new Cookie("aa", "ac", "/", "domain.com")
                                  };

            var result = await account.Storage.Cookie.LoadAsync();
            Assert.IsNull(result);

            await account.Storage.Cookie.SaveAsync(new CookieCollectionConverter(cookieContainer));
            result = await account.Storage.Cookie.LoadAsync();
            Assert.AreEqual(2, result.Cookies.Count);
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
//        [ClassCleanup]
//        public static void MyClassCleanup()
//        {
//
//        }

        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            if ( Directory.Exists("tests") )
            {
                var files = Directory.GetFiles("tests");
                foreach ( var file in files )
                {
                    File.Delete(file);
                }
            }
        }

        #endregion
    }
}