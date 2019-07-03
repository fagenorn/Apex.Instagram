using System;
using System.Threading.Tasks;

using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Response.JsonMap;

namespace Apex.Instagram.API.Request.Instagram
{
    internal class Direct : RequestCollection
    {
        public Direct(Account account) : base(account) { }

        public async Task<DirectRankedRecipientsResponse> GetRankedRecipients(string mode, bool showThreads, string query = null)
        {
            try
            {
                var request = new RequestBuilder(Account).SetUrl("direct_v2/ranked_recipients/")
                                                         .AddParam("mode", mode)
                                                         .AddParam("show_threads", showThreads)
                                                         .AddParam("use_unified_inbox", true);

                if ( query != null )
                {
                    request.AddParam("query", query);
                }

                return await Account.ApiRequest<DirectRankedRecipientsResponse>(request)
                                    .ConfigureAwait(false);
            }
            catch (ThrottledException)
            {
                // Throttling is so common that we'll simply return NULL in that case.
                return null;
            }
        }

        public async Task<DirectInboxResponse> GetInbox(string cursorId = null, int limit = 0)
        {
            if ( limit < 0 || limit > 20 )
            {
                throw new ArgumentException("Invalid value provided to limit.");
            }

            var request = new RequestBuilder(Account).SetUrl("direct_v2/inbox/")
                                                     .AddParam("persistentBadging", true)
                                                     .AddParam("visual_message_return_type", "unseen")
                                                     .AddParam("limit", limit);

            if ( cursorId != null )
            {
                request.AddParam("cursor", cursorId);
            }

            return await Account.ApiRequest<DirectInboxResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<PresencesResponse> GetPresences()
        {
            var request = new RequestBuilder(Account).SetUrl("direct_v2/get_presence/");

            return await Account.ApiRequest<PresencesResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}