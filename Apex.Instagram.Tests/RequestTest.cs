using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Apex.Instagram.Logger;
using Apex.Instagram.Request;
using Apex.Instagram.Tests.Maps;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Utf8Json;

namespace Apex.Instagram.Tests
{
    /// <summary>
    ///     Summary description for RequestTest
    /// </summary>
    [TestClass]
    public class RequestTest
    {
        private static readonly IApexLogger Logger = new ApexLogger(ApexLogLevel.Verbose);

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public async Task Cookies_Are_Created_On_Requests_Consitent()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies")
                                                     .SetAddDefaultHeaders(false)
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(0, (await account.Storage.Cookie.LoadAsync()).Cookies.Count);
            Assert.IsNull(account.GetCookie("freeform"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies/set")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false)
                                                 .AddParam("freeform", "test")
                                                 .Build();

            response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual("test", account.GetCookie("freeform"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual("test", account.GetCookie("freeform"));
        }

        [TestMethod]
        public async Task Cookies_Are_Stored_To_File_On_Request()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies/set")
                                                     .SetAddDefaultHeaders(false)
                                                     .SetNeedsAuth(false)
                                                     .AddParam("freeform", "test")
                                                     .Build();

            Assert.IsNull(await account.Storage.Cookie.LoadAsync());
            var response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual("test", account.GetCookie("freeform"));
            Assert.AreEqual(1, (await account.Storage.Cookie.LoadAsync()).Cookies.Count);

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(fileStorage)
                                                .BuildAsync();

            Assert.AreEqual(1, (await account.Storage.Cookie.LoadAsync()).Cookies.Count);
            Assert.AreEqual("test", account.GetCookie("freeform"));
        }

        [TestMethod]
        public async Task Temporary_Headers()
        {
            const string defaultHeaderKey   = "X-Ig-Bandwidth-Totalbytes-B";
            const string defaultHeaderValue = "0";

            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/headers")
                                                     .SetAddDefaultHeaders(false)
                                                     .SetNeedsAuth(false)
                                                     .AddHeader("Test", "best")
                                                     .Build();

            var response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var headersResponse = JsonSerializer.Deserialize<HeadersJsonMap>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("best", headersResponse.headers["Test"]);
            Assert.IsFalse(headersResponse.headers.ContainsKey(defaultHeaderKey));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/headers")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            headersResponse = JsonSerializer.Deserialize<HeadersJsonMap>(await response.Content.ReadAsStringAsync());
            Assert.IsFalse(headersResponse.headers.ContainsKey("Test"));
            Assert.IsFalse(headersResponse.headers.ContainsKey(defaultHeaderKey));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/headers")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            headersResponse = JsonSerializer.Deserialize<HeadersJsonMap>(await response.Content.ReadAsStringAsync());
            Assert.IsFalse(headersResponse.headers.ContainsKey("Test"));
            Assert.AreEqual(defaultHeaderValue, headersResponse.headers[defaultHeaderKey]);
        }

        [TestMethod]
        public async Task Post_Requests_Signed_Non_Singed_Parameters()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/post")
                                                     .SetNeedsAuth(false)
                                                     .AddPost("test", "best")
                                                     .AddPost("test2", "best2")
                                                     .AddPost("test3", "best3", false)
                                                     .Build();

            var response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"4", (string) postResponse["form"]["ig_sig_key_version"]);
            Assert.AreEqual(@"44605e27fe198f52599f9418b5a5c9d64fe985ffd02060a90c76d988d94dc64e.{""test"":""best"",""test2"":""best2""}", (string) postResponse["form"]["signed_body"]);
            Assert.AreEqual(@"best3", (string) postResponse["form"]["test3"]);
        }

        [TestMethod]
        public async Task Post_Requests_File_Upload()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var file        = @"tests/test_file.txt";
            var fileContent = Encoding.UTF8.GetBytes(@"hello");
            using (var fileStream = File.Create(file))
            {
                await fileStream.WriteAsync(fileContent, 0, fileContent.Length);
            }

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/post")
                                                     .SetNeedsAuth(false)
                                                     .AddFile("file_name", file)
                                                     .AddPost("test", "best")
                                                     .SetSignedPost(false)
                                                     .Build();

            var response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"hello", (string) postResponse["files"]["file_name"]);
            Assert.AreEqual(@"best", (string) postResponse["form"]["test"]);
        }

        [TestMethod]
        public async Task Get_Requests_Signed_Non_Singed_Parameters()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/get")
                                                     .SetNeedsAuth(false)
                                                     .AddParam("test", "best", true)
                                                     .AddParam("test2", "best2", true)
                                                     .AddParam("test3", "best3")
                                                     .SetSignedGet(true)
                                                     .Build();

            var response = await account.ApiRequest(request);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"4", (string) postResponse["args"]["ig_sig_key_version"]);
            Assert.AreEqual(@"44605e27fe198f52599f9418b5a5c9d64fe985ffd02060a90c76d988d94dc64e.{""test"":""best"",""test2"":""best2""}", (string) postResponse["args"]["signed_body"]);
            Assert.AreEqual(@"best3", (string) postResponse["args"]["test3"]);
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