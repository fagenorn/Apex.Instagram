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
                                                     .AddPost("prefetch", 1)
                                                     .AddPost("phone_id", Account.AccountInfo.PhoneId)
                                                     .AddPost("banner_token", "OgYA")
                                                     .AddPost("is_charging", "1")
                                                     .AddPost("will_sound_on", "1");

            return await Account.ApiRequest<TvGuideResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}