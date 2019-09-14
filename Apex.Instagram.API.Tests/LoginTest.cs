using System;
using System.Threading.Tasks;

using Apex.Instagram.API.Logger;
using Apex.Instagram.API.Login.Exception;
using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Tests.Extra;
using Apex.Shared.Model;

using Xunit;
using Xunit.Abstractions;

namespace Apex.Instagram.API.Tests
{
    public class LoginTest : IDisposable
    {
        public LoginTest(ITestOutputHelper output)
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

        private readonly FileStorage _fileStorage = new FileStorage(nameof(LoginTest));

        private void LoggerOnLogMessagePublished(object sender, ApexLogMessagePublishedEventArgs e) { _output.WriteLine(e.TraceMessage.ToString()); }

        private static readonly IApexLogger Logger = new ApexLogger(ApexLogLevel.Verbose);

        [Fact]
        public async Task Login_Challenge_Required()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("elytroposisLQw2")
                                                    .SetPassword("46tn2DE02")
                                                    .BuildAsync();

            Assert.False(account.LoginClient.LoginInfo.HasChallenge);
            var account1 = account;
            await Assert.ThrowsAsync<ChallengeException>(async () => await account1.LoginClient.ChallengeLogin());
            var account2 = account;
            await Assert.ThrowsAsync<ChallengeRequiredException>(async () => await account2.LoginClient.Login());
            Assert.True(account.LoginClient.LoginInfo.HasChallenge);
            var client   = await account.LoginClient.ChallengeLogin();
            var stepInfo = await client.Start();
            Assert.NotNull(client.ChallengeInfo.ApiPath);
            Assert.NotNull(stepInfo);
            var client1 = client;
            await Assert.ThrowsAsync<ChallengeException>(async () => await client1.DoNextStep("133123"));
            Assert.NotNull(client.ChallengeInfo.ApiPath);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .SetLogger(Logger)
                                                .SetUsername("elytroposisLQw2")
                                                .SetPassword("46tn2DE02")
                                                .BuildAsync();

            Assert.True(account.LoginClient.LoginInfo.HasChallenge);
            client = await account.LoginClient.ChallengeLogin();
            Assert.NotNull(client.ChallengeInfo.ApiPath);
            await Assert.ThrowsAsync<ChallengeException>(async () => await client.DoNextStep("133123"));
            await client.Start();
            Assert.Equal("Select verification method option:\r\n0: Phone (+7 *** ***-**-17)\r\n", stepInfo.Description);
            await Assert.ThrowsAsync<ChallengeException>(async () => await client.Replay());
            await Assert.ThrowsAsync<ChallengeException>(async () => await client.DoNextStep("1"));
            await Task.Delay(3000);
            stepInfo = await client.DoNextStep("0");
            Assert.Equal("Enter the 6 digit code that was sent to your mobile: +7 *** ***-26-17.\r\n", stepInfo.Description);
            await Task.Delay(3000);
            await client.Replay();
            Assert.Equal("Enter the 6 digit code that was sent to your mobile: +7 *** ***-26-17.\r\n", stepInfo.Description);
        }

        [Fact]
        public async Task Login_Correctly_Account_Is_Persistent()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("ladycomstock443")
                                                    .SetPassword("iMHh5GQ4")
                                                    .BuildAsync();

            Assert.Equal(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.True(account.LoginClient.LoginInfo.LastLogin.Passed);
            var response = await account.LoginClient.Login();
            Assert.True(response.IsOk());

            var info = await account.Storage.LoginInfo.LoadAsync();

            Assert.True(info.IsLoggedIn);
            Assert.NotEmpty(info.Experiments);
            Assert.NotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.False(account.LoginClient.LoginInfo.LastLogin.Passed);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .SetLogger(Logger)
                                                .BuildAsync();

            Assert.True(account.LoginClient.LoginInfo.IsLoggedIn);

            Assert.False(account.LoginClient.LoginInfo.LastLogin.Passed);
            Assert.NotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);

            response = await account.LoginClient.Login();
            Assert.True(response.IsOk());

            info = await account.Storage.LoginInfo.LoadAsync();

            Assert.True(info.IsLoggedIn);
            Assert.NotEmpty(info.Experiments);
            Assert.NotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.False(account.LoginClient.LoginInfo.LastLogin.Passed);

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .SetLogger(Logger)
                                                .BuildAsync();

            Assert.NotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.False(account.LoginClient.LoginInfo.LastLogin.Passed);
            account.LoginClient.LoginInfo.LastLogin = new LastAction(TimeSpan.FromMinutes(45));
            Assert.True(account.LoginClient.LoginInfo.LastLogin.Passed);
            Assert.Equal(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.Equal(TimeSpan.FromMinutes(30), account.LoginClient.LoginInfo.LastLogin.Limit);

            response = await account.LoginClient.Login();
            Assert.True(response.IsOk());

            Assert.NotEqual(0, account.LoginClient.LoginInfo.LastLogin.Last);
            Assert.False(account.LoginClient.LoginInfo.LastLogin.Passed);
        }

        [Fact]
        public async Task Login_No_Password()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("bob")
                                                    .BuildAsync();

            await Assert.ThrowsAsync<LoginException>(async () => await account.LoginClient.Login());
        }

        [Fact]
        public async Task Login_No_Username()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetPassword("bob")
                                                    .BuildAsync();

            await Assert.ThrowsAsync<LoginException>(async () => await account.LoginClient.Login());
        }

        [Fact]
        public async Task Login_Username_Doesnt_Exists()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("123TTT3281zzkii.0o")
                                                    .SetPassword("wrong")
                                                    .BuildAsync();

            await Assert.ThrowsAsync<InvalidUserException>(async () => await account.LoginClient.Login());
        }

        [Fact]
        public async Task Login_Wrong_Password()
        {
            var account = await new AccountBuilder().SetId(0)
                                                    .SetStorage(_fileStorage)
                                                    .SetLogger(Logger)
                                                    .SetUsername("bob")
                                                    .SetPassword("wrong")
                                                    .BuildAsync();

            var account1 = account;
            await Assert.ThrowsAsync<IncorrectPasswordException>(async () => await account1.LoginClient.Login());

            await account.UpdatePasswordAsync("new_password");

            var account2 = account;
            await Assert.ThrowsAsync<IncorrectPasswordException>(async () => await account2.LoginClient.Login());

            account.Dispose();

            account = await new AccountBuilder().SetId(0)
                                                .SetStorage(_fileStorage)
                                                .SetLogger(Logger)
                                                .SetUsername("bob")
                                                .SetPassword("wrong")
                                                .BuildAsync();

            await Assert.ThrowsAsync<IncorrectPasswordException>(async () => await account.LoginClient.Login());
        }
    }
}