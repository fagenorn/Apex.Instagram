﻿using System;
using System.Linq;
using System.Threading.Tasks;

using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Utils;

namespace Apex.Instagram.API.Request.Instagram
{
    internal class Discover : RequestCollection
    {
        public Discover(Account account) : base(account) { }

        public async Task<SuggestedSearchesResponse> GetSuggestedSearches(string type)
        {
            string[] allowed = {"blended", "users", "hashtags", "places"};

            if ( !allowed.Contains(type) )
            {
                throw new ArgumentException("Unknown search type.", type);
            }

            var request = new RequestBuilder(Account).SetUrl("fbsearch/suggested_searches/")
                                                     .AddParam("type", type);

            return await Account.ApiRequest<SuggestedSearchesResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<RecentSearchesResponse> GetRecentSearches()
        {
            var request = new RequestBuilder(Account).SetUrl("fbsearch/recent_searches/");

            return await Account.ApiRequest<RecentSearchesResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<ExploreResponse> GetExploreFeed(string maxId = null, bool isPrefetch = false)
        {
            var request = new RequestBuilder(Account).SetUrl("discover/topical_explore/")
                                                     .AddParam("is_prefetch", isPrefetch)
                                                     .AddParam("omit_cover_media", true)
                                                     .AddParam("use_sectional_payload", true)
                                                     .AddParam("timezone_offset", Time.Instance.GetTimezoneOffset())
                                                     .AddParam("session_id", Account.AccountInfo.SessionId)
                                                     .AddParam("include_fixed_destinations", true);

            if ( !isPrefetch )
            {
                if ( maxId != null )
                {
                    maxId = "0";
                }

                request.AddParam("max_id", maxId)
                       .AddParam("module", "explore_popular");
            }

            return await Account.ApiRequest<ExploreResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}