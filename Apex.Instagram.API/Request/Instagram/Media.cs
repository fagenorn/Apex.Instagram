using System.Threading.Tasks;

using Apex.Instagram.API.Response.JsonMap;

namespace Apex.Instagram.API.Request.Instagram
{
    internal class Media : RequestCollection
    {
        public Media(Account account) : base(account) { }

        public async Task<BlockedMediaResponse> GetBlockedMedia()
        {
            var request = new RequestBuilder(Account).SetUrl("media/blocked/");

            return await Account.ApiRequest<BlockedMediaResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}