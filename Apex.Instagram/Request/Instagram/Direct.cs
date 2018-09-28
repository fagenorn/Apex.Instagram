using System.Threading.Tasks;

using Apex.Instagram.Request.Exception;
using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Request.Instagram
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

                var a = await Account.ApiRequest<DirectRankedRecipientsResponse>(request.Build());
                return a;
            }
            catch (ThrottledException)
            {
                // Throttling is so common that we'll simply return NULL in that case.
                return null;
            }
        }

        public async Task<DirectInboxResponse> GetInbox(string cursorId = null)
        {
            var request = new RequestBuilder(Account).SetUrl("direct_v2/inbox/")
                                                     .AddParam("persistentBadging", true)
                                                     .AddParam("use_unified_inbox", true);

            if ( cursorId != null )
            {
                request.AddParam("cursor", cursorId);
            }

            return await Account.ApiRequest<DirectInboxResponse>(request.Build());
        }

        public async Task<PresencesResponse> GetPresences()
        {
            var request = new RequestBuilder(Account).SetUrl("direct_v2/get_presence/");

            return await Account.ApiRequest<PresencesResponse>(request.Build());
        }
    }
}