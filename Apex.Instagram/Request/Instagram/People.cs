using System.Threading.Tasks;

using Apex.Instagram.Request.Exception;
using Apex.Instagram.Response.JsonMap;

using Utf8Json;

namespace Apex.Instagram.Request.Instagram
{
    public class People : RequestCollection
    {
        internal People(Account account) : base(account) { }

        internal async Task<ActivityNewsResponse> GetRecentActivityInbox()
        {
            var request = new RequestBuilder(Account).SetUrl("news/inbox/");

            return await Account.ApiRequest<ActivityNewsResponse>(request.Build());
        }

        internal async Task<BootstrapUsersResponse> GetBootstrapUsers()
        {
            var surfaces = new[]
                           {
                               "coefficient_direct_closed_friends_ranking",
                               "coefficient_direct_recipients_ranking_variant_2",
                               "coefficient_rank_recipient_user_suggestion",
                               "coefficient_ios_section_test_bootstrap_ranking",
                               "autocomplete_user_list"
                           };

            try
            {
                var request = new RequestBuilder(Account).SetUrl("scores/bootstrap/users/")
                                                         .AddParam("surfaces", JsonSerializer.ToJsonString(surfaces));

                return await Account.ApiRequest<BootstrapUsersResponse>(request.Build());
            }
            catch (ThrottledException)
            {
                // Throttling is so common that we'll simply return NULL in that case.
                return null;
            }
        }

        public async Task<FriendshipResponse> Follow(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/create/{userId}/")
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("user_id", userId)
                                                     .AddPost("radio_type", "wifi-none");

            return await Account.ApiRequest<FriendshipResponse>(request.Build());
        }

        public async Task<FriendshipResponse> Unfollow(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/destroy/{userId}/")
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("user_id", userId)
                                                     .AddPost("radio_type", "wifi-none");

            return await Account.ApiRequest<FriendshipResponse>(request.Build());
        }

        public async Task<UserInfoResponse> GetInfoByName(string username, string module = null)
        {
            var request = new RequestBuilder(Account).SetUrl($"users/{username}/usernameinfo/");
            if ( module != null )
            {
                request.AddParam("from_module", module);
            }

            return await Account.ApiRequest<UserInfoResponse>(request.Build());
        }

        public async Task<UserInfoResponse> GetInfoById(ulong userId, string module = null)
        {
            var request = new RequestBuilder(Account).SetUrl($"users/{userId}/info/");
            if ( module != null )
            {
                request.AddParam("from_module", module);
            }

            return await Account.ApiRequest<UserInfoResponse>(request.Build());
        }

        public async Task<FriendshipsShowResponse> GetFriendship(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/show/{userId}/");

            return await Account.ApiRequest<FriendshipsShowResponse>(request.Build());
        }

        public async Task<FriendshipsShowManyResponse> GetFriendships(params ulong[] userIds)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/show_many/")
                                                     .SetSignedPost(false)
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("user_ids", string.Join(",", userIds))
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

            return await Account.ApiRequest<FriendshipsShowManyResponse>(request.Build());
        }
    }
}