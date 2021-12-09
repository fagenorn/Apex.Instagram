using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Apex.Instagram.API.Logger;
using Apex.Instagram.API.Request;
using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Request.Exception.EndpointException;
using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Response.Serializer;
using Apex.Instagram.API.Tests.Extra;
using Apex.Instagram.API.Tests.Maps;
using Apex.Instagram.API.Utils;
using Apex.Shared.Model;

using Xunit;
using Xunit.Abstractions;

using HttpClient = System.Net.Http.HttpClient;
using Version = Apex.Instagram.API.Constants.Version;

namespace Apex.Instagram.API.Tests
{
    public class RequestTest : IDisposable
    {
        public RequestTest(ITestOutputHelper output)
        {
            _output                    =  output;
            Logger.LogMessagePublished += LoggerOnLogMessagePublished;
        }

        public void Dispose()
        {
            Logger.LogMessagePublished -= LoggerOnLogMessagePublished;
            _fileStorage.ClearSave();
        }

        private readonly ITestOutputHelper _output;

        private readonly FileStorage _fileStorage = new FileStorage(nameof(RequestTest));

        private static readonly IApexLogger Logger = new ApexLogger(ApexLogLevel.Verbose);

        private HttpClient GetClient(Account account)
        {
            Debug.Assert(account.HttpClient != null, nameof(account.HttpClient) + " != null");

            var accClient = account.HttpClient;

            var type2   = typeof(Request.HttpClient);
            var client2 = type2.GetField("_request", BindingFlags.NonPublic | BindingFlags.Instance);

            Debug.Assert(client2 != null, nameof(client2) + " != null");

            return (HttpClient) client2.GetValue(accClient);
        }

        private void LoggerOnLogMessagePublished(object sender, ApexLogMessagePublishedEventArgs e) { _output.WriteLine(e.TraceMessage.ToString()); }

        [Fact]
        public async Task Bad_Request()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/status/400")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<BadRequestException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Cancel_Request_When_Account_Is_Disposed()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/1")
                                                     .SetNeedsAuth(false);

            var taskResult = account.ApiRequest<GenericResponse>(request);

            account.Dispose();

            var ex = await Assert.ThrowsAsync<RequestException>(async () => await taskResult);
            Assert.IsType<TaskCanceledException>(ex.InnerException);

            request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/1")
                                                 .SetNeedsAuth(false);

            await Assert.ThrowsAsync<ObjectDisposedException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Cancel_Request_When_Request_Client_Is_Disposed()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/1")
                                                     .SetNeedsAuth(false);

            var taskResult = account.ApiRequest<GenericResponse>(request);

            account.HttpClient.Dispose();

            var ex = await Assert.ThrowsAsync<RequestException>(async () => await taskResult);
            Assert.IsType<TaskCanceledException>(ex.InnerException);

            request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/1")
                                                 .SetNeedsAuth(false);

            await Assert.ThrowsAsync<ObjectDisposedException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Cookies_Are_Created_On_Requests_Consitent()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies")
                                                     .SetAddDefaultHeaders(false)
                                                     .SetNeedsAuth(false);

            var request1 = request;
            await Assert.ThrowsAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request1));

            Assert.Empty((await account.Storage.Cookie.LoadAsync()).Cookies);
            Assert.Null(account.GetCookie("freeform"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies/set")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false)
                                                 .AddParam("freeform", "test");

            var request2 = request;
            await Assert.ThrowsAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request2));

            Assert.Equal("test", account.GetCookie("freeform"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false);

            var request3 = request;
            await Assert.ThrowsAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request3));

            Assert.Equal("test", account.GetCookie("freeform"));
        }

        [Fact]
        public async Task Cookies_Are_Stored_To_File_On_Request()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/cookies/set")
                                                     .SetAddDefaultHeaders(false)
                                                     .SetNeedsAuth(false)
                                                     .AddParam("freeform", "test");

            Assert.Null(await account.Storage.Cookie.LoadAsync());
            var account1 = account;
            var request1 = request;
            await Assert.ThrowsAsync<EndpointException>(async () => await account1.ApiRequest<GenericResponse>(request1));

            Assert.Equal("test", account.GetCookie("freeform"));
            Assert.Single((await account.Storage.Cookie.LoadAsync()).Cookies);

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .BuildAsync();

            Assert.Single((await account.Storage.Cookie.LoadAsync()).Cookies);
            Assert.Equal("test", account.GetCookie("freeform"));
        }

        [Fact]
        public async Task Dispose_While_Request_Ongoing()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/3")
                                                     .SetNeedsAuth(false);

            var taskResult = account.ApiRequest<GenericResponse>(request);

            account.Dispose();
            var ex = await Assert.ThrowsAsync<RequestException>(async () => await taskResult);
            Assert.IsType<TaskCanceledException>(ex.InnerException);
        }

        [Fact]
        public async Task Generic_Api_Request_Status_Fail_Critical_Exception()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/j4y3y-1532627784/post")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<ForcedPasswordResetException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Generic_Api_Request_Status_Fail_With_Multiple_Error_Messages()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/1ikd3-1532626976/post")
                                                     .SetNeedsAuth(false);

            var exception = await Assert.ThrowsAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request));
            Assert.Equal("Select a valid choice. 0 is not one of the available choices.", exception.Message);
        }

        [Fact]
        public async Task Generic_Api_Request_Status_Fail_With_One_Error_Message()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/a1boc-1532620880/post")
                                                     .SetNeedsAuth(false);

            var exception = await Assert.ThrowsAsync<EndpointException>(async () => await account.ApiRequest<GenericResponse>(request));
            Assert.Equal("Some random message", exception.Message);
        }

        [Fact]
        public async Task Generic_Api_Request_Status_Ok()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/3e37m-1532618200/post")
                                                     .SetNeedsAuth(false);

            var result = await account.ApiRequest<GenericResponse>(request);
            Assert.Equal("ok", result.Status);
        }

        [Fact]
        public async Task Generic_Api_Request_Status_Ok_Critical_Status_Exception()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("http://ptsv2.com/t/2x2zs-1532628038/post")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<ThrottledException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Generic_Api_Request_With_IPv4_Proxy_Using_Authentication_Status_Ok()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetProxy(new Proxy("http://104.236.122.201:3128", "kash", "gevel22jj3"))
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.True(response.IsSuccessStatusCode);
            var postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
            Assert.Equal(@"104.236.122.201, 104.236.122.201", postResponse.GetProperty("origin").GetString());

            await account.UpdateProxyAsync(null);

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();
            Assert.True(response.IsSuccessStatusCode);
            postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
            Assert.NotEqual(@"104.236.122.201, 104.236.122.201", postResponse.GetProperty("origin").GetString());

            await account.UpdateProxyAsync(new Proxy("http://104.236.122.201:3128", "kash", "gevel22jj3"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();
            Assert.True(response.IsSuccessStatusCode);
            postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
            Assert.Equal(@"104.236.122.201, 104.236.122.201", postResponse.GetProperty("origin").GetString());
        }

        [Fact(Skip = "IPV6 not supported on network")]
        public async Task Generic_Api_Request_With_IPv6_Proxy_Using_Authentication_Status_Ok()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetProxy(new Proxy("http://[2604:a880:800:10::a:9001]:3128/", "kash", "gevel22jj3"))
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.True(response.IsSuccessStatusCode);
            var postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.Equal(@"104.236.122.201", (string) postResponse["origin"]);

            await account.UpdateProxyAsync(null);

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();
            Assert.True(response.IsSuccessStatusCode);
            postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(@"104.236.122.201", (string) postResponse["origin"]);

            await account.UpdateProxyAsync(new Proxy("http://[2604:a880:800:10::a:9001]:3128", "kash", "gevel22jj3"));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();
            Assert.True(response.IsSuccessStatusCode);
            postResponse = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            Assert.Equal(@"104.236.122.201", (string) postResponse["origin"]);
        }

        [Fact]
        public async Task Generic_Api_Request_With_Proxy_Needs_Authentication_Error()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetProxy(new Proxy("http://104.236.122.201:3128"))
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<ProxyAuthenticationRequiredException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Generic_Api_Request_With_Proxy_Wrong_Authentication_Error()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetProxy(new Proxy("http://104.236.122.201:3128", "kash", "wrong_password"))
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/ip")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<ProxyAuthenticationRequiredException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Get_Requests_Signed_Non_Singed_Parameters()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
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

            Assert.True(response.IsSuccessStatusCode);
            var postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
            var hash         = Hashing.Instance.ByteToString(Hashing.Instance.Sha256(@"{""test"":""best"",""test2"":""best2""}", Encoding.UTF8.GetBytes(Version.Instance.SigningKey)));
            var args = postResponse.GetProperty("args");

            Assert.Equal(@"4", args.GetProperty("ig_sig_key_version").GetString());
            Assert.Equal($"{hash}.{{\"test\":\"best\",\"test2\":\"best2\"}}", args.GetProperty("signed_body").GetString());
            Assert.Equal(@"best3", args.GetProperty("test3").GetString());
        }

        [Fact]
        public async Task Gzip_Request_content()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/anything")
                                                     .SetBody(new StringContent("hello"))
                                                     .SetNeedsAuth(false)
                                                     .Build();

            var response = await GetClient(account)
                               .SendAsync(request);

            var postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
            Assert.False(postResponse.GetProperty("headers").TryGetProperty("Content-Encoding", out _));

            Assert.Equal(@"hello", postResponse.GetProperty("data").GetString());

            request.Dispose();

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/anything")
                                                 .SetBody(new StringContent("hello", Encoding.UTF8))
                                                 .SetIsBodyCompressed(true)
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());

            Assert.True(postResponse.GetProperty("headers").TryGetProperty("Content-Encoding", out _));

            Assert.Equal(@"gzip", postResponse.GetProperty("headers").GetProperty("Content-Encoding").GetString());
            Assert.Equal(@"data:application/octet-stream;base64,H4sIAAAAAAAACstIzcnJBwCGphA2BQAAAA==", postResponse.GetProperty("data").GetString());

            request.Dispose();

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/anything")
                                                 .AddPost("hello", "hi")
                                                 .SetIsBodyCompressed(true)
                                                 .SetSignedPost(false)
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());

            Assert.True(postResponse.GetProperty("headers").TryGetProperty("Content-Encoding", out _));

            Assert.Equal(@"gzip", postResponse.GetProperty("headers").GetProperty("Content-Encoding").GetString());
            Assert.Equal(@"H4sIAAAAAAAACstIzcnJt83IBACN++pKCAAAAA==", Convert.ToBase64String(await request.Content.ReadAsByteArrayAsync()));

            request.Dispose();

            var temp = Path.GetTempFileName();

            File.WriteAllText(temp, "superduperbigsecret");

            var build = new RequestBuilder(account).SetUrl("https://httpbin.org/anything")
                                                   .AddFile("test", temp, "test")
                                                   .SetIsBodyCompressed(true)
                                                   .SetSignedPost(false)
                                                   .SetNeedsAuth(false);

            response = await GetClient(account)
                           .SendAsync(build.Build());

            postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());

            Assert.True(postResponse.GetProperty("headers").TryGetProperty("Content-Encoding", out _));

            Assert.Equal(@"gzip", postResponse.GetProperty("headers").GetProperty("Content-Encoding").GetString());

            request.Dispose();
            build.Dispose();
            File.Delete(temp);
        }

        [Fact]
        public async Task Only_One_Request_At_A_Time()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            int? lastFinished = null;

            var request = new RequestBuilder(account).SetUrl("http://httpbin.org/delay/2")
                                                     .SetNeedsAuth(false);

            var taskResult = account.ApiRequest<GenericResponse>(request)
                                    .ContinueWith(x => lastFinished = 1);

            var request2 = new RequestBuilder(account).SetUrl("http://httpbin.org/get")
                                                      .SetNeedsAuth(false);

            var taskResult2 = account.ApiRequest<GenericResponse>(request2)
                                     .ContinueWith(x => lastFinished = 2);

            await taskResult2;
            await taskResult;

            Assert.NotNull(lastFinished);
            Assert.Equal(2, lastFinished.Value);
        }

        [Fact]
        public async Task Post_Requests_File_Upload()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync()
                                                    .ConfigureAwait(false);

            string file        = $@"{nameof(RequestTest)}/test_file.txt";
            var          fileContent = Encoding.UTF8.GetBytes(@"hello");
            await using (var fileStream = File.Create(file))
            {
                await fileStream.WriteAsync(fileContent, 0, fileContent.Length)
                                .ConfigureAwait(false);
            }

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/post")
                                                     .SetNeedsAuth(false)
                                                     .AddFile("file_name", file)
                                                     .AddPost("test", "best")
                                                     .SetSignedPost(false)
                                                     .Build();

            await account.Logger.Debug<HttpClient>(request);
            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.True(response.IsSuccessStatusCode);
            var postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
            Assert.Equal(@"hello", postResponse.GetProperty("files").GetProperty("file_name").GetString());
            Assert.Equal(@"best", postResponse.GetProperty("form").GetProperty("test").GetString());
        }

        [Fact]
        public async Task Post_Requests_Signed_Non_Singed_Parameters()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/post")
                                                     .SetNeedsAuth(false)
                                                     .AddPost("test", "best")
                                                     .AddPost("test2", "best2")
                                                     .AddPost("test3", "best3", false)
                                                     .Build();

            await account.Logger.Debug<HttpClient>(request);
            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.True(response.IsSuccessStatusCode);
            var postResponse = await JsonSerializer.DeserializeAsync<JsonElement>(await response.Content.ReadAsStreamAsync());
            var hash         = Hashing.Instance.ByteToString(Hashing.Instance.Sha256(@"{""test"":""best"",""test2"":""best2""}", Encoding.UTF8.GetBytes(Version.Instance.SigningKey)));
            var args = postResponse.GetProperty("form");

            Assert.Equal(@"4", args.GetProperty("ig_sig_key_version").GetString());
            Assert.Equal($"{hash}.{{\"test\":\"best\",\"test2\":\"best2\"}}", args.GetProperty("signed_body").GetString());
            Assert.Equal(@"best3", args.GetProperty("test3").GetString());
        }

        [Fact]
        public async Task Proxy_Authentication_Required()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/status/407")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<ProxyAuthenticationRequiredException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Request_DNS_Error()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://non-existent-website-123-host-dns.org/")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<RequestException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task RequestHeadersTooLarge_Request()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/status/431")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<RequestHeadersTooLargeException>(async () => await account.ApiRequest<GenericResponse>(request));
        }

        [Fact]
        public async Task Temporary_Headers()
        {
            const string defaultHeaderKey   = "X-Ig-Bandwidth-Totalbytes-B";
            const string defaultHeaderValue = "0";

            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/headers")
                                                     .SetAddDefaultHeaders(false)
                                                     .SetNeedsAuth(false)
                                                     .AddHeader("Test", "best")
                                                     .Build();

            var response = await GetClient(account)
                               .SendAsync(request);

            request.Dispose();

            Assert.True(response.IsSuccessStatusCode);
            var headersResponse = await JsonSerializer.DeserializeAsync<HeadersJsonMap>(await response.Content.ReadAsStreamAsync(), JsonSerializerDefaultOptions.Instance);
            Assert.Equal("best", headersResponse.Headers["Test"]);
            Assert.False(headersResponse.Headers.ContainsKey(defaultHeaderKey));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/headers")
                                                 .SetAddDefaultHeaders(false)
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();

            Assert.True(response.IsSuccessStatusCode);
            headersResponse = await JsonSerializer.DeserializeAsync<HeadersJsonMap>(await response.Content.ReadAsStreamAsync(), JsonSerializerDefaultOptions.Instance);
            Assert.False(headersResponse.Headers.ContainsKey("Test"));
            Assert.False(headersResponse.Headers.ContainsKey(defaultHeaderKey));

            request = new RequestBuilder(account).SetUrl("https://httpbin.org/headers")
                                                 .SetNeedsAuth(false)
                                                 .Build();

            response = await GetClient(account)
                           .SendAsync(request);

            request.Dispose();

            Assert.True(response.IsSuccessStatusCode);
            headersResponse = await JsonSerializer.DeserializeAsync<HeadersJsonMap>(await response.Content.ReadAsStreamAsync(), JsonSerializerDefaultOptions.Instance);
            Assert.False(headersResponse.Headers.ContainsKey("Test"));
            Assert.Equal(defaultHeaderValue, headersResponse.Headers[defaultHeaderKey]);
        }

        [Fact]
        public async Task Throttled_Request()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .BuildAsync();

            var request = new RequestBuilder(account).SetUrl("https://httpbin.org/status/429")
                                                     .SetNeedsAuth(false);

            await Assert.ThrowsAsync<ThrottledException>(async () => await account.ApiRequest<GenericResponse>(request));
        }
    }
}