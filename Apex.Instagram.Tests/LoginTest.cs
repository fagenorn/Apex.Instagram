using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using Apex.Instagram.Logger;
using Apex.Instagram.Login.Exception;
using Apex.Instagram.Model.Internal;
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

            var account1 = account;
            await Assert.ThrowsExceptionAsync<IncorrectPasswordException>(async () => await account1.LoginClient.Login());

            await account.UpdatePasswordAsync("new_password");

            var account2 = account;
            await Assert.ThrowsExceptionAsync<IncorrectPasswordException>(async () => await account2.LoginClient.Login());

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(fileStorage)
                                                .SetLogger(Logger)
                                                .SetUsername("bob")
                                                .SetPassword("wrong")
                                                .BuildAsync();

            await Assert.ThrowsExceptionAsync<IncorrectPasswordException>(async () => await account.LoginClient.Login());
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

            await Assert.ThrowsExceptionAsync<InvalidUserException>(async () => await account.LoginClient.Login());
        }

        [TestMethod]
        public async Task Login_No_Password()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("bob")
                                                    .BuildAsync();

            await Assert.ThrowsExceptionAsync<LoginException>(async () => await account.LoginClient.Login());
        }

        [TestMethod]
        public async Task Login_No_Username()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetPassword("bob")
                                                    .BuildAsync();

            await Assert.ThrowsExceptionAsync<LoginException>(async () => await account.LoginClient.Login());
        }

        [TestMethod]
        public async Task Login_Challenge_Required()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("elytroposisLQw2")
                                                    .SetPassword("46tn2DE02")
                                                    .BuildAsync();

            Assert.IsFalse(account.LoginClient.LoginInfo.HasChallenge);
            var account1 = account;
            await Assert.ThrowsExceptionAsync<ChallengeException>(async () => await account1.LoginClient.ChallengeLogin());
            var account2 = account;
            await Assert.ThrowsExceptionAsync<ChallengeRequiredException>(async () => await account2.LoginClient.Login());
            Assert.IsTrue(account.LoginClient.LoginInfo.HasChallenge);
            var client = await account.LoginClient.ChallengeLogin();
            await client.Reset();
            Assert.IsNotNull(client.ChallengeInfo.Url);
            var stepInfo = client.GetNextStep();
            Assert.IsNotNull(stepInfo);
            var client1 = client;
            await Assert.ThrowsExceptionAsync<ChallengeException>(async () => await client1.DoNextStep("133123"));
            Assert.IsNotNull(client.ChallengeInfo.Url);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(fileStorage)
                                                .SetLogger(Logger)
                                                .SetUsername("elytroposisLQw2")
                                                .SetPassword("46tn2DE02")
                                                .BuildAsync();

            Assert.IsTrue(account.LoginClient.LoginInfo.HasChallenge);
            client = await account.LoginClient.ChallengeLogin();
            Assert.IsNotNull(client.ChallengeInfo.Url);
            Assert.ThrowsException<ChallengeException>(() => client.GetNextStep());
            await Assert.ThrowsExceptionAsync<ChallengeException>(async () => await client.DoNextStep("133123"));
            await client.Reset();
            client.GetNextStep();
            Assert.AreEqual("Select verification method option:\r\n0: Phone (+7 *** ***-**-17)\r\n", stepInfo.Description);
            await Assert.ThrowsExceptionAsync<ChallengeException>(async () => await client.Replay());
            await Assert.ThrowsExceptionAsync<ChallengeException>(async () => await client.DoNextStep("1"));
            await client.DoNextStep("0");
            Assert.AreEqual("Enter a valid phone number.\r\nCurrent phone number: None.\r\n", stepInfo.Description);
            await Assert.ThrowsExceptionAsync<ChallengeException>(async () => await client.Replay());
            await client.DoNextStep("0489494882");
            stepInfo = client.GetNextStep();
            await Task.Delay(3000);
            await client.Replay();
            Assert.AreEqual("Enter the 6 digit code that was sent to your mobile: +32 489 49 48 82.", stepInfo.Description);
        }

        [TestMethod]
        public async Task Login_Correctly_Account_Is_Persistent()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("ladycomstock443")
                                                    .SetPassword("iMHh5GQ4")
                                                    .BuildAsync();

            Assert.AreEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.IsTrue(account.LoginClient.LoginInfo.LastLogin.Passed);
            await account.LoginClient.Login();

            var info = await account.Storage.LoginInfo.LoadAsync();

            Assert.IsTrue(info.IsLoggedIn);
            Assert.AreNotEqual(0, info.Experiments);
            Assert.AreNotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.IsFalse(account.LoginClient.LoginInfo.LastLogin.Passed);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(fileStorage)
                                                .SetLogger(Logger)
                                                .BuildAsync();

            Assert.IsTrue(account.LoginClient.LoginInfo.IsLoggedIn);

            Assert.IsFalse(account.LoginClient.LoginInfo.LastLogin.Passed);
            Assert.AreNotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);

            await account.LoginClient.Login();

            info = await account.Storage.LoginInfo.LoadAsync();

            Assert.IsTrue(info.IsLoggedIn);
            Assert.AreNotEqual(0, info.Experiments);
            Assert.AreNotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.IsFalse(account.LoginClient.LoginInfo.LastLogin.Passed);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(fileStorage)
                                                .SetLogger(Logger)
                                                .BuildAsync();

            Assert.AreNotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.IsFalse(account.LoginClient.LoginInfo.LastLogin.Passed);
            account.LoginClient.LoginInfo.LastLogin = new LastAction(TimeSpan.FromMinutes(45));
            Assert.IsTrue(account.LoginClient.LoginInfo.LastLogin.Passed);
            Assert.AreEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.AreEqual(TimeSpan.FromMinutes(30), account.LoginClient.LoginInfo.LastLogin.Limit);

            await account.LoginClient.Login();
            Assert.AreNotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.IsFalse(account.LoginClient.LoginInfo.LastLogin.Passed);
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