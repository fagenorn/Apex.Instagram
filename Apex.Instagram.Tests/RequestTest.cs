using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Apex.Instagram.Logger;
using Apex.Instagram.Model.Request;
using Apex.Instagram.Request;
using Apex.Instagram.Request.Exception;
using Apex.Instagram.Request.Exception.EndpointException;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Tests.Maps;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Utf8Json;

using HttpClient = System.Net.Http.HttpClient;

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
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies")
                                                     .SetAddDefaultHeaders(false)
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var request1 = request;
            await Assert.ThrowsExceptionAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request1));

            Assert.AreEqual(0, (await account.Storage.Cookie.LoadAsync()).Cookies.Count);
            Assert.IsNull(account.GetCookie("freeform"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies/set")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false)
                                                 .AddParam("freeform", "test")
                                                 .Build();

            var request2 = request;
            await Assert.ThrowsExceptionAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request2));

            Assert.AreEqual("test", account.GetCookie("freeform"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false)
                                                 .Build();

            var request3 = request;
            await Assert.ThrowsExceptionAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request3));

            Assert.AreEqual("test", account.GetCookie("freeform"));
        }

        [TestMethod]
        public async Task Cookies_Are_Stored_To_File_On_Request()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies/set")
                                                     .SetAddDefaultHeaders(false)
                                                     .SetNeedsAuth(false)
                                                     .AddParam("freeform", "test")
                                                     .Build();

            Assert.IsNull(await account.Storage.Cookie.LoadAsync());
            var account1 = account;
            var request1 = request;
            await Assert.ThrowsExceptionAsync<EndpointException>(async () => await account1.ApiRequest<GenericResponse>(request1));

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

            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.IsTrue(response.IsSuccessStatusCode);
            var headersResponse = JsonSerializer.Deserialize<HeadersJsonMap>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("best", headersResponse.headers["Test"]);
            Assert.IsFalse(headersResponse.headers.ContainsKey(defaultHeaderKey));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/headers")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();

            Assert.IsTrue(response.IsSuccessStatusCode);
            headersResponse = JsonSerializer.Deserialize<HeadersJsonMap>(await response.Content.ReadAsStringAsync());
            Assert.IsFalse(headersResponse.headers.ContainsKey("Test"));
            Assert.IsFalse(headersResponse.headers.ContainsKey(defaultHeaderKey));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/headers")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();

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

            account.Logger.Debug<HttpClient>(request);
            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.IsTrue(response.IsSuccessStatusCode);
            var postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"4", (string) postResponse["form"]["ig_sig_key_version"]);
            Assert.AreEqual(@"a50cbc3219ec82fb34a3e6ddeb52467a9324853ed6071c40aaaa2987bfdb6bd1.{""test"":""best"",""test2"":""best2""}", (string) postResponse["form"]["signed_body"]);
            Assert.AreEqual(@"best3", (string) postResponse["form"]["test3"]);
        }

        private HttpClient GetClient(Account account)
        {
            Debug.Assert(account.HttpClient != null, nameof(account.HttpClient) + " != null");

            var accClient = account.HttpClient;

            var type2   = typeof(Request.HttpClient);
            var client2 = type2.GetField("_request", BindingFlags.NonPublic | BindingFlags.Instance);

            Debug.Assert(client2 != null, nameof(client2) + " != null");

            return (HttpClient) client2.GetValue(accClient);
        }

        [TestMethod]
        public async Task Post_Requests_File_Upload()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync().ConfigureAwait(false);

            var file        = @"tests/test_file.txt";
            var fileContent = Encoding.UTF8.GetBytes(@"hello");
            using (var fileStream = File.Create(file))
            {
                await fileStream.WriteAsync(fileContent, 0, fileContent.Length).ConfigureAwait(false);
            }

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/post")
                                                     .SetNeedsAuth(false)
                                                     .AddFile("file_name", file)
                                                     .AddPost("test", "best")
                                                     .SetSignedPost(false)
                                                     .Build();

            account.Logger.Debug<HttpClient>(request);
            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

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

            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.IsTrue(response.IsSuccessStatusCode);
            var postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"4", (string) postResponse["args"]["ig_sig_key_version"]);
            Assert.AreEqual(@"a50cbc3219ec82fb34a3e6ddeb52467a9324853ed6071c40aaaa2987bfdb6bd1.{""test"":""best"",""test2"":""best2""}", (string) postResponse["args"]["signed_body"]);
            Assert.AreEqual(@"best3", (string) postResponse["args"]["test3"]);
        }

        [TestMethod]
        public async Task Bad_Request()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/status/400")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await Assert.ThrowsExceptionAsync<BadRequestException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [TestMethod]
        public async Task Proxy_Authentication_Required()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/status/407")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await Assert.ThrowsExceptionAsync<ProxyAuthenticationRequiredException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [TestMethod]
        public async Task Dispose_While_Request_Ongoing()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/1")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var taskResult = account.ApiRequest<GenericResponse>(request);

            request.Dispose();
            var ex = await Assert.ThrowsExceptionAsync<RequestException>(async () => await taskResult);
            Assert.IsInstanceOfType(ex.InnerException, typeof(ObjectDisposedException));
        }

        [TestMethod]
        public async Task Cancel_Request_Ongoing()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/1")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var taskResult = account.ApiRequest<GenericResponse>(request);

            account.Dispose();
            var ex = await Assert.ThrowsExceptionAsync<RequestException>(async () => await taskResult);
            Assert.IsInstanceOfType(ex.InnerException, typeof(OperationCanceledException));

            request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/1")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await Assert.ThrowsExceptionAsync<ObjectDisposedException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [TestMethod]
        [ExpectedException(typeof(ThrottledException))]
        public async Task Throttled_Request()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/status/429")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await account.ApiRequest<GenericResponse>(request);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestHeadersTooLargeException))]
        public async Task RequestHeadersTooLarge_Request()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/status/431")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await account.ApiRequest<GenericResponse>(request);
        }

        [TestMethod]
        public async Task Only_One_Request_At_A_Time()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            int? lastFinished = null;

            var request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/2")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var taskResult = account.ApiRequest<GenericResponse>(request).ContinueWith(x => lastFinished = 1);

            var request2 = new RequestBuilder(account).SetUrl("http://httpbin.org/get")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var taskResult2 = account.ApiRequest<GenericResponse>(request2).ContinueWith(x => lastFinished = 2);

            await taskResult2;
            await taskResult;

            Assert.IsNotNull(lastFinished);
            Assert.AreEqual(2, lastFinished.Value);
        }

        [TestMethod]
        public async Task Request_DNS_Error()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://non-existent-website-123-host-dns.org/")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await Assert.ThrowsExceptionAsync<RequestException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [TestMethod]
        public async Task Generic_Api_Request_With_Proxy_Wrong_Authentication_Error()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetProxy(new Proxy("http://104.236.122.201:3128", "kash", "wrong_password"))
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await Assert.ThrowsExceptionAsync<ProxyAuthenticationRequiredException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [TestMethod]
        public async Task Generic_Api_Request_With_Proxy_Needs_Authentication_Error()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetProxy(new Proxy("http://104.236.122.201:3128"))
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await Assert.ThrowsExceptionAsync<ProxyAuthenticationRequiredException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [TestMethod]
        public async Task Generic_Api_Request_With_IPv4_Proxy_Using_Authentication_Status_Ok()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetProxy(new Proxy("http://104.236.122.201:3128", "kash", "gevel22jj3"))
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.IsTrue(response.IsSuccessStatusCode);
            var postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"104.236.122.201", (string) postResponse["origin"]);

            await account.UpdateProxy(null);

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();
            Assert.IsTrue(response.IsSuccessStatusCode);
            postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreNotEqual(@"104.236.122.201", (string) postResponse["origin"]);

            await account.UpdateProxy(new Proxy("http://104.236.122.201:3128", "kash", "gevel22jj3"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();
            Assert.IsTrue(response.IsSuccessStatusCode);
            postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"104.236.122.201", (string) postResponse["origin"]);
        }

        [TestMethod]
        public async Task Generic_Api_Request_With_IPv6_Proxy_Using_Authentication_Status_Ok()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetProxy(new Proxy("http://[2604:a880:800:10::a:9001]:3128/", "kash", "gevel22jj3"))
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.IsTrue(response.IsSuccessStatusCode);
            var postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"104.236.122.201", (string) postResponse["origin"]);

            await account.UpdateProxy(null);

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();
            Assert.IsTrue(response.IsSuccessStatusCode);
            postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreNotEqual(@"104.236.122.201", (string) postResponse["origin"]);

            await account.UpdateProxy(new Proxy("http://[2604:a880:800:10::a:9001]:3128", "kash", "gevel22jj3"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();
            Assert.IsTrue(response.IsSuccessStatusCode);
            postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(@"104.236.122.201", (string) postResponse["origin"]);
        }

        [TestMethod]
        public async Task Generic_Api_Request_Status_Ok()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/3e37m-1532618200/post")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var result = await account.ApiRequest<GenericResponse>(request);
            Assert.AreEqual("ok", result.Status);
        }

        [TestMethod]
        public async Task Generic_Api_Request_Status_Fail_With_One_Error_Message()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/a1boc-1532620880/post")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var exception = await Assert.ThrowsExceptionAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request));
            Assert.AreEqual("Some random message", exception.Message);
        }

        [TestMethod]
        public async Task Generic_Api_Request_Status_Fail_With_Multiple_Error_Messages()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/1ikd3-1532626976/post")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var exception = await Assert.ThrowsExceptionAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request));
            Assert.AreEqual("Select a valid choice. 0 is not one of the available choices.", exception.Message);
        }

        [TestMethod]
        public async Task Generic_Api_Request_Status_Fail_Critical_Exception()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/j4y3y-1532627784/post")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await Assert.ThrowsExceptionAsync<ForcedPasswordResetException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [TestMethod]
        public async Task Generic_Api_Request_Status_Ok_Critical_Status_Exception()
        {
            var fileStorage = new FileStorage();
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/2x2zs-1532628038/post")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            await Assert.ThrowsExceptionAsync<ThrottledException>(async () => await account.ApiRequest<GenericResponse>(request));
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