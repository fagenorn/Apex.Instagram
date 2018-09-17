using System.Collections.Generic;
using System.Threading.Tasks;

using Apex.Instagram.Constants;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Response.JsonMap.Model;

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

        public async Task<SyncResponse> SyncDeviceFeatures(bool preLogin = false)
        {
            var request = new RequestBuilder(Account).SetUrl("qe/sync/")
                                                     .AddHeader("X-DEVICE-ID", Account.AccountInfo.Uuid)
                                                     .AddPost("id", Account.AccountInfo.Uuid)
                                                     .AddPost("experiments", Version.Instance.LoginExperiments);

            if ( preLogin )
            {
                request.SetNeedsAuth(false);
            }
            else
            {
                request.AddPost("_uuid", Account.AccountInfo.Uuid)
                       .AddPost("_uid", Account.AccountInfo.AccountId)
                       .AddPost("_csrftoken", Account.LoginClient.CsrfToken);
            }

            return await Account.ApiRequest<SyncResponse>(request.Build());
        }

        public async Task<SyncResponse> SyncUserFeatures()
        {
            var request = new RequestBuilder(Account).SetUrl("qe/sync/")
                                                     .AddHeader("X-DEVICE-ID", Account.AccountInfo.Uuid)
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("id", Account.AccountInfo.AccountId)
                                                     .AddPost("experiments", Version.Instance.Experiments);

            var response = await Account.ApiRequest<SyncResponse>(request.Build());
            await SaveExperiments(response);

            return response;
        }

        public async Task<LauncherSyncResponse> SendLauncherSync(bool preLogin)
        {
            var request = new RequestBuilder(Account).SetUrl("launcher/sync/")
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("configs", string.Empty);

            if ( preLogin )
            {
                request.SetNeedsAuth(false)
                       .AddPost("id", Account.AccountInfo.Uuid);
            }
            else
            {
                request.AddPost("id", Account.AccountInfo.AccountId)
                       .AddPost("_uuid", Account.AccountInfo.Uuid)
                       .AddPost("_uid", Account.AccountInfo.AccountId);
            }

            return await Account.ApiRequest<LauncherSyncResponse>(request.Build());
        }

        public async Task<GenericResponse> LogAttribution()
        {
            var request = new RequestBuilder(Account).SetUrl("attribution/log_attribution/")
                                                     .SetNeedsAuth(false)
                                                     .AddPost("adid", Account.AccountInfo.AdvertisingId);

            return await Account.ApiRequest<GenericResponse>(request.Build());
        }

        public async Task<TokenResultResponse> FetchZeroRatingToken(string reason = "token_expired")
        {
            var request = new RequestBuilder(Account).SetUrl("zr/token/result/")
                                                     .SetNeedsAuth(false)
                                                     .AddParam("custom_device_id", Account.AccountInfo.Uuid)
                                                     .AddParam("device_id", Account.AccountInfo.DeviceId)
                                                     .AddParam("fetch_reason", reason)
                                                     .AddParam("token_hash", Account.LoginClient.LoginInfo.ZrToken);

            var response = await Account.ApiRequest<TokenResultResponse>(request.Build());
            await SaveZeroRatingToken(response.Token);

            return response;
        }

        private async Task SaveZeroRatingToken(Token token = null)
        {
            if ( token == null )
            {
                return;
            }

            var rules = new Dictionary<string, string>();
            foreach ( var rule in token.RewriteRules )
            {
                rules[rule.Matcher] = rule.Replacer;
            }

            Account.HttpClient.ZeroRatingMiddleware.Update(rules);
            Account.LoginClient.LoginInfo.ZrRules   = rules;
            Account.LoginClient.LoginInfo.ZrToken   = token.TokenHash;
            Account.LoginClient.LoginInfo.ZrExpires = token.ExpiresAt();

            await Account.Storage.LoginInfo.SaveAsync(Account.LoginClient.LoginInfo);
        }

        private async Task SaveExperiments(SyncResponse syncResponse)
        {
            var experiments = new Dictionary<string, Dictionary<string, string>>();
            foreach ( var experiment in syncResponse.Experiments )
            {
                var group   = experiment.Name;
                var @params = experiment.Params;

                if ( group == null || @params == null )
                {
                    continue;
                }

                if ( !experiments.ContainsKey(group) )
                {
                    experiments[group] = new Dictionary<string, string>();
                }

                foreach ( var param in @params )
                {
                    var paramName = param.Name;
                    if ( paramName == null )
                    {
                        continue;
                    }

                    experiments[group][paramName] = param.Value;
                }
            }

            Account.LoginClient.LoginInfo.Experiments = experiments;
            Account.LoginClient.LoginInfo.LastExperiments.Update();

            await Account.Storage.LoginInfo.SaveAsync(Account.LoginClient.LoginInfo);
        }
    }
}