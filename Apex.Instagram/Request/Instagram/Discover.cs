using System;
using System.Linq;
using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Request.Instagram
{
    internal class Discover : RequestCollection
    {
        public Discover(Account account) : base(account) { }

        public async Task<SuggestedSearchesResponse> GetSuggestedSearches(string type)
        {
            string[] allowed =
            {
                "blended",
                "users",
                "hashtags",
                "places"
            };

            if ( !allowed.Contains(type) )
            {
                throw new ArgumentException("Unknown search type.", type);
            }

            var request = new RequestBuilder(Account).SetUrl("fbsearch/suggested_searches/")
                                                     .AddParam("type", type);

            return await Account.ApiRequest<SuggestedSearchesResponse>(request.Build());
        }

        public async Task<RecentSearchesResponse> GetRecentSearches()
        {
            var request = new RequestBuilder(Account).SetUrl("fbsearch/recent_searches/");

            return await Account.ApiRequest<RecentSearchesResponse>(request.Build());
        }
    }
}