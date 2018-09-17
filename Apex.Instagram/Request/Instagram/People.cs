using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Request.Instagram
{
    internal class People : RequestCollection
    {
        public People(Account account) : base(account) { }

        public async Task<ActivityNewsResponse> GetRecentActivityInbox()
        {
            var request = new RequestBuilder(Account).SetUrl("news/inbox/");

            return await Account.ApiRequest<ActivityNewsResponse>(request.Build());
        }
    }
}