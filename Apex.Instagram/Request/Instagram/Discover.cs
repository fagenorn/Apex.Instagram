using System;
using System.Linq;
using System.Threading.Tasks;

using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Utils;

using Utf8Json;

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

            return await Account.ApiRequest<SuggestedSearchesResponse>(request.Build()).ConfigureAwait(false);
        }

        public async Task<RecentSearchesResponse> GetRecentSearches()
        {
            var request = new RequestBuilder(Account).SetUrl("fbsearch/recent_searches/");

            return await Account.ApiRequest<RecentSearchesResponse>(request.Build()).ConfigureAwait(false);
        }

        public async Task<ExploreResponse> GetExploreFeed(string maxId = null, bool isPrefetch = false)
        {
            var request = new RequestBuilder(Account).SetUrl("discover/explore/")
                                                     .AddParam("is_prefetch", isPrefetch)
                                                     .AddParam("is_from_promote", false)
                                                     .AddParam("timezone_offset", Time.Instance.GetTimezoneOffset())
                                                     .AddParam("session_id", Account.AccountInfo.SessionId)
                                                     .AddParam("supported_capabilities_new", JsonSerializer.ToJsonString(Constants.Request.Instance.SupportedCapabilities));

            if ( isPrefetch )
            {
                if ( maxId != null )
                {
                    maxId = "0";
                }

                request.AddParam("max_id", maxId)
                       .AddParam("module", "explore_popular");
            }

            return await Account.ApiRequest<ExploreResponse>(request.Build()).ConfigureAwait(false);
        }
    }
}