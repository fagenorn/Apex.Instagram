using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Apex.Instagram.API.Exception;
using Apex.Instagram.API.Logger;
using Apex.Instagram.API.Response.JsonMap.Model;
using Apex.Instagram.API.Tests.Extra;

using Xunit;

namespace Apex.Instagram.API.Tests
{
    public class InteractionFixture : IDisposable, IAsyncLifetime
    {
        private readonly FileStorage _fileStorage = new FileStorage(nameof(InteractionFixture));

        private readonly IApexLogger _logger = new ApexLogger(ApexLogLevel.Verbose);

        public InteractionFixture() { _logger.LogMessagePublished += LoggerOnLogMessagePublished; }

        public Account Account { get; private set; }

        public async Task InitializeAsync()
        {
            Account = await new AccountBuilder().SetId(1)
                                                .SetStorage(_fileStorage)
                                                .SetLogger(_logger)
                                                .SetUsername("ladycomstock443")
                                                .SetPassword("iMHh5GQ4")
                                                .BuildAsync();

            await Account.LoginClient.Login();
        }

        public Task DisposeAsync() { return Task.CompletedTask; }

        public void Dispose()
        {
            Account.Dispose();
            _fileStorage.ClearSave();
        }

        private static void LoggerOnLogMessagePublished(object sender, ApexLogMessagePublishedEventArgs e) { Debug.WriteLine(e.TraceMessage); }
    }

    public class InteractionTests : IClassFixture<InteractionFixture>
    {
        public InteractionTests(InteractionFixture fixture) { _fixture = fixture; }

        private readonly InteractionFixture _fixture;

        [Fact]
        public async Task Follow_Unfollow_User()
        {
            var celebInfo = await _fixture.Account.People.GetInfoByNameAsync("goofball_9");
            Assert.NotNull(celebInfo.User.Pk);
            Assert.Equal(3232737714u, celebInfo.User.Pk);
            Assert.Equal("goofball_9", celebInfo.User.Username);

            var unfollow = await _fixture.Account.People.UnfollowAsync(celebInfo.User.Pk.Value);
            Assert.NotNull(unfollow.FriendshipStatus.Following);
            Assert.False(unfollow.FriendshipStatus.Following.Value);

            var friendship = await _fixture.Account.People.GetFriendshipAsync(celebInfo.User.Pk.Value);
            Assert.NotNull(friendship.Following);
            Assert.False(friendship.Following.Value);

            var follow = await _fixture.Account.People.FollowAsync(celebInfo.User.Pk.Value);
            Assert.NotNull(follow.FriendshipStatus.Following);
            Assert.True(follow.FriendshipStatus.Following.Value);

            var friendships = await _fixture.Account.People.GetFriendshipsAsync(celebInfo.User.Pk.Value);
            Assert.NotNull(friendships.FriendshipStatuses);
            Assert.Single(friendships.FriendshipStatuses);
            Assert.NotNull(friendships.FriendshipStatuses["3232737714"]
                                      .Following);

            Assert.True(friendships.FriendshipStatuses["3232737714"]
                                   .Following.Value);

            unfollow = await _fixture.Account.People.UnfollowAsync(celebInfo.User.Pk.Value);
            Assert.NotNull(unfollow.FriendshipStatus.Following);
            Assert.False(unfollow.FriendshipStatus.Following.Value);
        }

        [Fact]
        public async Task Paginate_Throug_Users_Followers()
        {
            var selenaInfo = await _fixture.Account.People.GetInfoByNameAsync("selenagomez");
            Assert.NotNull(selenaInfo.User.Pk);
            Assert.Equal(460563723u, selenaInfo.User.Pk);
            Assert.Equal("selenagomez", selenaInfo.User.Username);

            var followers = _fixture.Account.People.GetFollowersPaginator(selenaInfo.User.Pk.Value);

            const int numOfPages = 3;
            var       page       = 0;
            User      firstUser  = null;

            while ( page != numOfPages )
            {
                Assert.True(followers.HasMore);
                var reponse = await followers.NextAsync();
                Assert.NotNull(reponse.Users);
                Assert.NotEmpty(reponse.Users);
                Assert.NotEqual(firstUser?.Pk, reponse.Users[0]
                                                      .Pk);

                firstUser = reponse.Users[0];
                page++;
            }

            var zzInfo = await _fixture.Account.People.GetInfoByNameAsync("zzzzzzzzz");
            Assert.NotNull(zzInfo.User.Pk);
            Assert.Equal(617778u, zzInfo.User.Pk);
            Assert.Equal("zzzzzzzzz", zzInfo.User.Username);

            followers = _fixture.Account.People.GetFollowersPaginator(zzInfo.User.Pk.Value);
            Assert.True(followers.HasMore);
            var resp = await followers.NextAsync();
            Assert.NotNull(resp.Users);
            Assert.NotEmpty(resp.Users);
            Assert.False(followers.HasMore);
            var followers1 = followers;
            await Assert.ThrowsAsync<EndOfPageException>(async () => await followers1.NextAsync());

            var fefeInfo = await _fixture.Account.People.GetInfoByNameAsync("fefefefefefefefe");
            Assert.NotNull(fefeInfo.User.Pk);
            Assert.Equal(252852361u, fefeInfo.User.Pk);
            Assert.Equal("fefefefefefefefe", fefeInfo.User.Username);

            followers = _fixture.Account.People.GetFollowersPaginator(fefeInfo.User.Pk.Value);
            Assert.True(followers.HasMore);
            var resp2 = await followers.NextAsync();
            Assert.NotNull(resp2.Users);
            Assert.Empty(resp2.Users);
            await Assert.ThrowsAsync<EndOfPageException>(async () => await followers.NextAsync());
        }
    }
}