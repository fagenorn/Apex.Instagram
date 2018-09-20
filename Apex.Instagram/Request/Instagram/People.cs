using System.Threading.Tasks;

using Apex.Instagram.Request.Exception;
using Apex.Instagram.Response.JsonMap;

using Utf8Json;

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

        public async Task<BootstrapUsersResponse> GetBootstrapUsers()
        {
            var surfaces = new[]
                           {
                               "coefficient_direct_closed_friends_ranking",
                               "coefficient_direct_recipients_ranking_variant_2",
                               "coefficient_rank_recipient_user_suggestion",
                               "coefficient_ios_section_test_bootstrap_ranking",
                               "autocomplete_user_list"
                           };

            try
            {
                var request = new RequestBuilder(Account).SetUrl("scores/bootstrap/users/")
                                                         .AddParam("surfaces", JsonSerializer.ToJsonString(surfaces));

                return await Account.ApiRequest<BootstrapUsersResponse>(request.Build());
            }
            catch (ThrottledException)
            {
                // Throttling is so common that we'll simply return NULL in that case.
                return null;
            }
        }
    }
}