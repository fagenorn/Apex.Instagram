using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Request.Instagram
{
    internal class Tv : RequestCollection
    {
        public Tv(Account account) : base(account) { }

        public async Task<TvGuideResponse> GetTvGuide()
        {
            var request = new RequestBuilder(Account).SetUrl("igtv/tv_guide/")
                                                     .AddParam("prefetch", 1)
                                                     .AddParam("phone_id", Account.AccountInfo.PhoneId)
                                                     .AddParam("banner_token", "OgYA")
                                                     .AddParam("is_charging", "1")
                                                     .AddParam("will_sound_on", "1");

            return await Account.ApiRequest<TvGuideResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}