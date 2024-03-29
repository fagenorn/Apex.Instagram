﻿using System.Text.Json;
using System.Threading.Tasks;

using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Response.Serializer;

namespace Apex.Instagram.API.Request.Instagram
{
    internal class Story : RequestCollection
    {
        public Story(Account account) : base(account) { }

        public async Task<ReelsTrayFeedResponse> GetReelsTrayFeed()
        {
            var request = new RequestBuilder(Account).SetUrl("feed/reels_tray/")
                                                     .SetSignedPost(false)
                                                     .AddPost("supported_capabilities_new", JsonSerializer.Serialize(Constants.Request.Instance.SupportedCapabilities, JsonSerializerDefaultOptions.Instance))
                                                     .AddPost("reason", "pull_to_refresh")
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid);

            return await Account.ApiRequest<ReelsTrayFeedResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}