using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Request.Instagram
{
    internal class Media : RequestCollection
    {
        public Media(Account account) : base(account) { }

        public async Task<BlockedMediaResponse> GetBlockedMedia()
        {
            var request = new RequestBuilder(Account).SetUrl("media/blocked/");

            return await Account.ApiRequest<BlockedMediaResponse>(request.Build).ConfigureAwait(false);
        }
    }
}