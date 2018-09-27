using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using Apex.Instagram.Logger;
using Apex.Instagram.Request.Exception;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apex.Instagram.Tests
{
    /// <summary>
    ///     Summary description for LoginTest
    /// </summary>
    [TestClass]
    public class LoginTest
    {
        private static readonly IApexLogger Logger = new ApexLogger(ApexLogLevel.Verbose);

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public async Task Login_Wrong_Password()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("bob")
                                                    .SetPassword("wrong")
                                                    .BuildAsync();

            await Assert.ThrowsExceptionAsync<IncorrectPasswordException>(async () => await account.Login());
        }

        [TestMethod]
        public async Task Login_Username_Doesnt_Exists()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("123TTT3281zzkii.0o")
                                                    .SetPassword("wrong")
                                                    .BuildAsync();

            await Assert.ThrowsExceptionAsync<InvalidUserException>(async () => await account.Login());
        }

        [TestMethod]
        public async Task Login_Challenge_Required()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("RZ3YnN4")
                                                    .SetPassword("w8EXjH1")
                                                    .BuildAsync();

            await Assert.ThrowsExceptionAsync<ChallengeRequiredException>(async () => await account.Login());
        }

        [TestMethod]
        public async Task Login_Correctly_Account_Is_Persistent()
        {

            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("Lottie.Cheetham1Np")
                                                    .SetPassword("wYPs2PP6")
                                                    .BuildAsync();

            await account.Login();

            var info = await account.Storage.LoginInfo.LoadAsync();

            Assert.IsTrue(info.IsLoggedIn);
            Assert.AreNotEqual(0, info.Experiments);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(fileStorage)
                                                .SetLogger(Logger)
                                                .BuildAsync();

            Assert.IsTrue(account.LoginClient.LoginInfo.IsLoggedIn);

            await account.Login();

            info = await account.Storage.LoginInfo.LoadAsync();

            Assert.IsTrue(info.IsLoggedIn);
            Assert.AreNotEqual(0, info.Experiments);
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext) { Logger.LogMessagePublished += LoggerOnLogMessagePublished; }

        private static void LoggerOnLogMessagePublished(object sender, ApexLogMessagePublishedEventArgs e) { Debug.WriteLine(e.TraceMessage); }

        //
        // Use ClassCleanup to run code after all tests in a class have run
        //        [ClassCleanup]
        //        public static void MyClassCleanup()
        //        {
        //
        //        }

        //
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