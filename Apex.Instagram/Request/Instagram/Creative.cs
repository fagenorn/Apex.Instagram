using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;

using Utf8Json;

namespace Apex.Instagram.Request.Instagram
{
    internal class Creative : RequestCollection
    {
        public Creative(Account account) : base(account) { }

        public async Task<WriteSuppotedCapabilitiesResponse> SendSupportedCapabilities()
        {
            var request = new RequestBuilder(Account).SetUrl("creatives/write_supported_capabilities/")
                                                     .AddPost("supported_capabilities_new", JsonSerializer.ToJsonString(Constants.Request.Instance.SupportedCapabilities))
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

            return await Account.ApiRequest<WriteSuppotedCapabilitiesResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}