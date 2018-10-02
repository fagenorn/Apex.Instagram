using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Request.Instagram
{
    internal class Profile : RequestCollection
    {
        public Profile(Account account) : base(account) { }

        public async Task<GenericResponse> SetContactPointPrefill(string usage)
        {
            var request = new RequestBuilder(Account).SetUrl("accounts/contact_point_prefill/")
                                                     .SetNeedsAuth(false)
                                                     .AddPost("phone_id", Account.AccountInfo.PhoneId)
                                                     .AddPost("usage", usage)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

            return await Account.ApiRequest<GenericResponse>(request.Build()).ConfigureAwait(false);
        }
    }
}