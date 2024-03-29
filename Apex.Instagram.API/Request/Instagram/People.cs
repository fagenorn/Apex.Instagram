﻿using System.Text.Json;
using System.Threading.Tasks;

using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Request.Instagram.Paginate;
using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Response.Serializer;

namespace Apex.Instagram.API.Request.Instagram
{
    /// <summary>Request collection of all people related requests.</summary>
    public class People : RequestCollection
    {
        internal People(Account account) : base(account) { }

        internal async Task<ActivityNewsResponse> GetRecentActivityInbox()
        {
            var request = new RequestBuilder(Account).SetUrl("news/inbox/");

            return await Account.ApiRequest<ActivityNewsResponse>(request)
                                .ConfigureAwait(false);
        }

        internal async Task<BootstrapUsersResponse> GetBootstrapUsers()
        {
            var surfaces = new[]
                           {
                               "autocomplete_user_list",
                               "coefficient_besties_list_ranking",
                               "coefficient_rank_recipient_user_suggestion",
                               "coefficient_ios_section_test_bootstrap_ranking",
                               "coefficient_direct_recipients_ranking_variant_2"
                           };

            try
            {
                var request = new RequestBuilder(Account).SetUrl("scores/bootstrap/users/")
                                                         .AddParam("surfaces", JsonSerializer.Serialize(surfaces, JsonSerializerDefaultOptions.Instance));

                return await Account.ApiRequest<BootstrapUsersResponse>(request)
                                    .ConfigureAwait(false);
            }
            catch (ThrottledException)
            {
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
        public async Task<FriendshipResponse> FollowAsync(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/create/{userId}/")
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("user_id", userId)
                                                     .AddPost("radio_type", "wifi-none");

            return await Account.ApiRequest<FriendshipResponse>(request)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Unfollow a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        ///     <see cref="FriendshipResponse" />
        /// </returns>
        public async Task<FriendshipResponse> UnfollowAsync(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/destroy/{userId}/")
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("user_id", userId)
                                                     .AddPost("radio_type", "wifi-none");

            return await Account.ApiRequest<FriendshipResponse>(request)
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
        public async Task<UserInfoResponse> GetInfoByNameAsync(string username, string module = null)
        {
            var request = new RequestBuilder(Account).SetUrl($"users/{username}/usernameinfo/");
            if ( module != null )
            {
                request.AddParam("from_module", module);
            }

            return await Account.ApiRequest<UserInfoResponse>(request)
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
        public async Task<UserInfoResponse> GetInfoByIdAsync(ulong userId, string module = null)
        {
            var request = new RequestBuilder(Account).SetUrl($"users/{userId}/info/");
            if ( module != null )
            {
                request.AddParam("from_module", module);
            }

            return await Account.ApiRequest<UserInfoResponse>(request)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Get the status of your friendship with a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        ///     <see cref="FriendshipsShowResponse" />
        /// </returns>
        public async Task<FriendshipsShowResponse> GetFriendshipAsync(ulong userId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/show/{userId}/");

            return await Account.ApiRequest<FriendshipsShowResponse>(request)
                                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Get the status of your friendship with a list of users.
        /// </summary>
        /// <param name="userIds">A list of user ids.</param>
        /// <returns>
        ///     <see cref="FriendshipsShowManyResponse" />
        /// </returns>
        public async Task<FriendshipsShowManyResponse> GetFriendshipsAsync(params ulong[] userIds)
        {
            var request = new RequestBuilder(Account).SetUrl("friendships/show_many/")
                                                     .SetSignedPost(false)
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("user_ids", string.Join(",", userIds))
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

            return await Account.ApiRequest<FriendshipsShowManyResponse>(request)
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

            return await Account.ApiRequest<FollowerAndFollowingResponse>(request)
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
        public IAutoPaginate<FollowerAndFollowingResponse> GetFollowersPaginator(ulong userId, string searchQuery = null) { return new AutoPaginateWithRankToken<FollowerAndFollowingResponse>(parameters => GetFollowers(userId, parameters.rankToken, searchQuery, parameters.maxId)); }

        private async Task<FollowerAndFollowingResponse> GetFollowing(ulong userId, string rankToken, string searchQuery, string maxId)
        {
            var request = new RequestBuilder(Account).SetUrl($"friendships/{userId}/following/")
                                                     .AddParam("includes_hashtags", true)
                                                     .AddParam("rank_token", rankToken);

            if ( searchQuery != null )
            {
                request.AddParam("query", searchQuery);
            }

            if ( maxId != null )
            {
                request.AddParam("max_id", maxId);
            }

            return await Account.ApiRequest<FollowerAndFollowingResponse>(request)
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
        public IAutoPaginate<FollowerAndFollowingResponse> GetFollowingPaginator(ulong userId, string searchQuery = null) { return new AutoPaginateWithRankToken<FollowerAndFollowingResponse>(parameters => GetFollowing(userId, parameters.rankToken, searchQuery, parameters.maxId)); }

        /// <summary>Gets a list of ranked users to disaply in Android's share UI.</summary>
        /// <returns>
        ///     <see cref="SharePrefillResponse" />
        /// </returns>
        public async Task<SharePrefillResponse> GetSharePrefill()
        {
            var request = new RequestBuilder(Account).SetUrl("banyan/banyan/")
                                                     .AddParam("views", "[\"story_share_sheet\",\"threads_people_picker\",\"reshare_share_sheet\"]");

            return await Account.ApiRequest<SharePrefillResponse>(request)
                                .ConfigureAwait(false);
        }

        /// <summary>Get user details about your own account.</summary>
        /// <returns>
        ///     <see cref="UserInfoResponse" />
        /// </returns>
        public async Task<UserInfoResponse> GetSelfInfo()
        {
            return await GetInfoByIdAsync(Account.AccountInfo.AccountId)
                       .ConfigureAwait(false);
        }
    }
}