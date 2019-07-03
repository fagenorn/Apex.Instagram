using System;
using System.Threading.Tasks;

using Apex.Shared.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apex.Instagram.API.Tests
{
    /// <summary>
    ///     Summary description for UtilsTest
    /// </summary>
    [TestClass]
    public class UtilsTest
    {
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Epoch_Test()
        {
            const double epochTime  = 1532344691d;
            const double epochTime2 = 91d;
            var          currentEpochTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            var          epoch            = new Epoch();
            Assert.IsTrue(currentEpochTime - epoch.Value >= 0);
            Assert.IsTrue(currentEpochTime - epoch.Value <= 15);

            epoch = new Epoch(epochTime);
            var epoch2 = new Epoch(epochTime2);

            Assert.AreEqual(1532344600d, (epoch - epoch2).Value);
        }

        [TestMethod]
        public async Task Last_Action_Test()
        {
            var wait = TimeSpan.FromMilliseconds(10);
            var action = new LastAction(wait);

            Assert.IsTrue(action.Passed);
            action.Update();
            Assert.IsFalse(action.Passed);
            await Task.Delay(wait + TimeSpan.FromMilliseconds(5));
            Assert.IsTrue(action.Passed);


            action = new LastAction(wait, new Epoch());

            Assert.IsFalse(action.Passed);
            await Task.Delay(wait + TimeSpan.FromMilliseconds(5));
            Assert.IsTrue(action.Passed);
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
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
    }
}