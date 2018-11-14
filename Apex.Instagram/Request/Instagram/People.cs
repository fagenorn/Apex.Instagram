using System.Threading.Tasks;

using Apex.Instagram.Request.Exception;
using Apex.Instagram.Request.Instagram.Paginate;
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

            return await Account.ApiRequest<ActivityNewsResponse>(request.Build)
                                .ConfigureAwait(false);
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

                return await Account.ApiRequest<BootstrapUsersResponse>(request.Build)
                                    .ConfigureAwait(false);
            }
            catch (ThrottledException)
            {
                // Throttling is so common that we'll simply return NULL in that case.
                return null;
            }
        }

        /// <summary>
        ///     Follow a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        ///     <see cref="FriendshipResponse" />
        /// </returns>
        public async Task<FriendshipResponse> Follow(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/create/{userId}/")
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("user_id", userId)
                                                     .AddPost("radio_type", "wifi-none");

            return await Account.ApiRequest<FriendshipResponse>(request.Build)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Unfollow a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        ///     <see cref="FriendshipResponse" />
        /// </returns>
        public async Task<FriendshipResponse> Unfollow(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/destroy/{userId}/")
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("user_id", userId)
                                                     .AddPost("radio_type", "wifi-none");

            return await Account.ApiRequest<FriendshipResponse>(request.Build)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Get more information of a user based on their username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="module">
        ///     From which app module (page) you have opened the profile. One of (incomplete):
        ///     "comment_likers",
        ///     "comment_owner",
        ///     "followers",
        ///     "following",
        ///     "likers_likers_media_view_profile",
        ///     "likers_likers_photo_view_profile",
        ///     "likers_likers_video_view_profile",
        ///     "newsfeed",
        ///     "self_followers",
        ///     "self_following",
        ///     "self_likers_self_likers_media_view_profile",
        ///     "self_likers_self_likers_photo_view_profile",
        ///     "self_likers_self_likers_video_view_profile".
        /// </param>
        /// <returns>
        ///     <see cref="UserInfoResponse" />
        /// </returns>
        public async Task<UserInfoResponse> GetInfoByName(string username, string module = null)
        {
            var request = new RequestBuilder(Account).SetUrl($"users/{username}/usernameinfo/");
            if ( module != null )
            {
                request.AddParam("from_module", module);
            }

            return await Account.ApiRequest<UserInfoResponse>(request.Build)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Get more information of a user based on their id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="module">
        ///     From which app module (page) you have opened the profile. One of (incomplete):
        ///     "comment_likers",
        ///     "comment_owner",
        ///     "followers",
        ///     "following",
        ///     "likers_likers_media_view_profile",
        ///     "likers_likers_photo_view_profile",
        ///     "likers_likers_video_view_profile",
        ///     "newsfeed",
        ///     "self_followers",
        ///     "self_following",
        ///     "self_likers_self_likers_media_view_profile",
        ///     "self_likers_self_likers_photo_view_profile",
        ///     "self_likers_self_likers_video_view_profile".
        /// </param>
        /// <returns>
        ///     <see cref="UserInfoResponse" />
        /// </returns>
        public async Task<UserInfoResponse> GetInfoById(ulong userId, string module = null)
        {
            var request = new RequestBuilder(Account).SetUrl($"users/{userId}/info/");
            if ( module != null )
            {
                request.AddParam("from_module", module);
            }

            return await Account.ApiRequest<UserInfoResponse>(request.Build)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Get the status of your friendship with a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        ///     <see cref="FriendshipsShowResponse" />
        /// </returns>
        public async Task<FriendshipsShowResponse> GetFriendship(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/show/{userId}/");

            return await Account.ApiRequest<FriendshipsShowResponse>(request.Build)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Get the status of your friendship with a list of users.
        /// </summary>
        /// <param name="userIds">A list of user ids.</param>
        /// <returns>
        ///     <see cref="FriendshipsShowManyResponse" />
        /// </returns>
        public async Task<FriendshipsShowManyResponse> GetFriendships(params ulong[] userIds)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/show_many/")
                                                     .SetSignedPost(false)
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("user_ids", string.Join(",", userIds))
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

            return await Account.ApiRequest<FriendshipsShowManyResponse>(request.Build)
                                .ConfigureAwait(false);
        }

        private async Task<FollowerAndFollowingResponse> GetFollowers(ulong userId, string rankToken, string searchQuery, string maxId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/{userId}/followers/")
                                                     .AddParam("rank_token", rankToken);

            if ( searchQuery != null )
            {
                request.AddParam("query", searchQuery);
            }

            if ( maxId != null )
            {
                request.AddParam("max_id", maxId);
            }

            return await Account.ApiRequest<FollowerAndFollowingResponse>(request.Build)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Get a list of a users followers.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="searchQuery">Limit the userlist to ones matching the query.</param>
        /// <returns>
        ///     <see cref="IAutoPaginate&lt;FollowerAndFollowingResponse&gt;" />
        /// </returns>
        public IAutoPaginate<FollowerAndFollowingResponse> GetFollowers(ulong userId, string searchQuery = null) { return new AutoPaginateWithRankToken<FollowerAndFollowingResponse>(parameters => GetFollowers(userId, parameters.rankToken, searchQuery, parameters.maxId)); }

        private async Task<FollowerAndFollowingResponse> GetFollowing(ulong userId, string rankToken, string searchQuery, string maxId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/{userId}/following/")
                                                     .AddParam("rank_token", rankToken);

            if ( searchQuery != null )
            {
                request.AddParam("query", searchQuery);
            }

            if ( maxId != null )
            {
                request.AddParam("max_id", maxId);
            }

            return await Account.ApiRequest<FollowerAndFollowingResponse>(request.Build)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Get a list of a users followings.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="searchQuery">Limit the userlist to ones matching the query.</param>
        /// <returns>
        ///     <see cref="IAutoPaginate&lt;FollowerAndFollowingResponse&gt;" />
        /// </returns>
        public IAutoPaginate<FollowerAndFollowingResponse> GetFollowing(ulong userId, string searchQuery = null) { return new AutoPaginateWithRankToken<FollowerAndFollowingResponse>(parameters => GetFollowing(userId, parameters.rankToken, searchQuery, parameters.maxId)); }
    }
}