using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Request.Instagram
{
    internal class Internal : RequestCollection
    {
        public Internal(Account account) : base(account) { }

        public async Task<MsisdnHeaderResponse> ReadMsisdnHeader(string usage, string subnoKey = null)
        {
            var request = new RequestBuilder(Account).SetUrl("accounts/read_msisdn_header/")
                                                     .SetNeedsAuth(false)
                                                     .AddHeader("X-DEVICE-ID", Account.AccountInfo.Uuid)
                                                     .AddPost("device_id", Account.AccountInfo.Uuid)
                                                     .AddPost("mobile_subno_usage", usage);

            if ( subnoKey != null )
            {
                request.AddPost("subno_key", subnoKey);
            }

            return await Account.ApiRequest<MsisdnHeaderResponse>(request.Build());
        }
    }
}