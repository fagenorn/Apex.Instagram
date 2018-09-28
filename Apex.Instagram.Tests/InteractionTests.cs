﻿using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using Apex.Instagram.Exception;
using Apex.Instagram.Logger;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apex.Instagram.Tests
{
    /// <summary>
    ///     Summary description for InteractionTests
    /// </summary>
    [TestClass]
    public class InteractionTests
    {
        private static readonly IApexLogger Logger = new ApexLogger(ApexLogLevel.Verbose);

        private static Account _account;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static async Task MyClassInitialize(TestContext testContext)
        {
            Logger.LogMessagePublished += LoggerOnLogMessagePublished;

            var fileStorage = new FileStorage();
            _account = await new AccountBuilder().SetId(0)
                                                .SetStorage(fileStorage)
                                                .SetLogger(Logger)
                                                .SetUsername("Lottie.Cheetham1Np")
                                                .SetPassword("wYPs2PP6")
                                                .BuildAsync();

            await _account.Login();
        }

        private static void LoggerOnLogMessagePublished(object sender, ApexLogMessagePublishedEventArgs e) { Debug.WriteLine(e.TraceMessage); }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
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

        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public async Task Follow_Unfollow_User()
        {
            var celebInfo = await _account.People.GetInfoByName("celeb_syles");
            Assert.IsNotNull(celebInfo.User.Pk);
            Assert.AreEqual(3549147650u, celebInfo.User.Pk);
            Assert.AreEqual("celeb_syles", celebInfo.User.Username);

            var unfollow = await _account.People.Unfollow(celebInfo.User.Pk.Value);
            Assert.IsNotNull(unfollow.FriendshipStatus.Following);
            Assert.IsFalse(unfollow.FriendshipStatus.Following.Value);

            var friendship = await _account.People.GetFriendship(celebInfo.User.Pk.Value);
            Assert.IsNotNull(friendship.Following);
            Assert.IsFalse(friendship.Following.Value);

            var follow = await _account.People.Follow(celebInfo.User.Pk.Value);
            Assert.IsNotNull(follow.FriendshipStatus.Following);
            Assert.IsTrue(follow.FriendshipStatus.Following.Value);

            var friendships = await _account.People.GetFriendships(celebInfo.User.Pk.Value);
            Assert.IsNotNull(friendships.FriendshipStatuses);
            Assert.AreEqual(1, friendships.FriendshipStatuses.Count);
            Assert.IsNotNull(friendships.FriendshipStatuses["3549147650"]
                                        .Following);

            Assert.IsTrue(friendships.FriendshipStatuses["3549147650"]
                                     .Following.Value);

            unfollow = await _account.People.Unfollow(celebInfo.User.Pk.Value);
            Assert.IsNotNull(unfollow.FriendshipStatus.Following);
            Assert.IsFalse(unfollow.FriendshipStatus.Following.Value);
        }

        [TestMethod]
        public async Task Paginate_Throug_Users_Followers()
        {
            var selenaInfo = await _account.People.GetInfoByName("selenagomez");
            Assert.IsNotNull(selenaInfo.User.Pk);
            Assert.AreEqual(460563723u, selenaInfo.User.Pk);
            Assert.AreEqual("selenagomez", selenaInfo.User.Username);

            var followers = _account.People.GetFollowers(selenaInfo.User.Pk.Value);

            const int numOfPages = 3;
            var page = 0;

            while ( page != numOfPages )
            {
                Assert.IsTrue(followers.HasMore);
                var reponse = await followers.Next();
                Assert.IsNotNull(reponse.Users);
                Assert.AreNotEqual(0, reponse.Users.Length);
                page++;
            }

            var zzInfo = await _account.People.GetInfoByName("zzzzzzzzz");
            Assert.IsNotNull(zzInfo.User.Pk);
            Assert.AreEqual(617778u, zzInfo.User.Pk);
            Assert.AreEqual("zzzzzzzzz", zzInfo.User.Username);

            followers = _account.People.GetFollowers(zzInfo.User.Pk.Value);
            Assert.IsTrue(followers.HasMore);
            var resp = await followers.Next();
            Assert.IsNotNull(resp.Users);
            Assert.AreNotEqual(0, resp.Users.Length);
            Assert.IsFalse(followers.HasMore);
            var followers1 = followers;
            await Assert.ThrowsExceptionAsync<EndOfPageException>(async () => await followers1.Next());


            var fefeInfo = await _account.People.GetInfoByName("fefefefefefefefe");
            Assert.IsNotNull(fefeInfo.User.Pk);
            Assert.AreEqual(252852361u, fefeInfo.User.Pk);
            Assert.AreEqual("fefefefefefefefe", fefeInfo.User.Username);

            followers = _account.People.GetFollowers(fefeInfo.User.Pk.Value);
            Assert.IsTrue(followers.HasMore);
            var resp2 = await followers.Next();
            Assert.IsNotNull(resp2.Users);
            Assert.AreEqual(0, resp2.Users.Length);
            await Assert.ThrowsExceptionAsync<EndOfPageException>(async () => await followers.Next());
        }
    }
}