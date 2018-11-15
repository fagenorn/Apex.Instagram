using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;

using Utf8Json;

namespace Apex.Instagram.Request.Instagram
{
    internal class Story : RequestCollection
    {
        public Story(Account account) : base(account) { }

        public async Task<ReelsTrayFeedResponse> GetReelsTrayFeed()
        {
            var request = new RequestBuilder(Account).SetUrl("feed/reels_tray/")
                                                     .SetSignedPost(false)
                                                     .AddPost("supported_capabilities_new", JsonSerializer.ToJsonString(Constants.Request.Instance.SupportedCapabilities))
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

            return await Account.ApiRequest<ReelsTrayFeedResponse>(request.Build)
                                .ConfigureAwait(false);
        }
    }
}