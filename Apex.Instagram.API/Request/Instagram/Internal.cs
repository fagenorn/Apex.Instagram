using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using Apex.Instagram.API.Constants;
using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Request.Signature;
using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Response.JsonMap.Model;
using Apex.Instagram.API.Response.Serializer;

namespace Apex.Instagram.API.Request.Instagram
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

            return await Account.ApiRequest<MsisdnHeaderResponse>(request)
                                .ConfigureAwait(false);
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

            return await Account.ApiRequest<SyncResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<ProfileNoticeResponse> GetProfileNotice()
        {
            var request = new RequestBuilder(Account).SetUrl("users/profile_notice/");

            return await Account.ApiRequest<ProfileNoticeResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<CapabilitiesDecisionsResponse> GetDeviceCapabilitiesDecisions()
        {
            var request = new RequestBuilder(Account).SetUrl("device_capabilities/decisions/")
                                                     .AddParam("signed_body", Signer.GenerateSignature("{}.{}"))
                                                     .AddParam("ig_sig_key_version", Version.Instance.SigningKeyVersion);

            return await Account.ApiRequest<CapabilitiesDecisionsResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<LoomFetchConfigResponse> GetLoomFetchConfig()
        {
            var request = new RequestBuilder(Account).SetUrl("loom/fetch_config/");

            return await Account.ApiRequest<LoomFetchConfigResponse>(request)
                                .ConfigureAwait(false);
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

            var response = await Account.ApiRequest<SyncResponse>(request)
                                        .ConfigureAwait(false);

            await SaveExperiments(response)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<LauncherSyncResponse> SendLauncherSync(bool preLogin)
        {
            try
            {
                var request = new RequestBuilder(Account).SetUrl("launcher/sync/")
                                                         .AddPost("configs", Version.Instance.LauncherConfigs);

                if ( preLogin )
                {
                    request.SetNeedsAuth(false)
                           .AddPost("id", Account.AccountInfo.Uuid);
                }
                else
                {
                    request.AddPost("id", Account.AccountInfo.AccountId)
                           .AddPost("_uuid", Account.AccountInfo.Uuid)
                           .AddPost("_uid", Account.AccountInfo.AccountId)
                           .AddPost("_csrftoken", Account.LoginClient.CsrfToken);
                }

                return await Account.ApiRequest<LauncherSyncResponse>(request)
                                    .ConfigureAwait(false);
            }
            catch (ConsentRequiredException)
            {
                // Caused for accounts created before 05/24/2018 logging from europe who haven't accepted GDPR changes.
                return null;
            }
        }

        public async Task<GenericResponse> LogAttribution()
        {
            var request = new RequestBuilder(Account).SetUrl("attribution/log_attribution/")
                                                     .SetNeedsAuth(false)
                                                     .AddPost("adid", Account.AccountInfo.AdvertisingId);

            return await Account.ApiRequest<GenericResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<TokenResultResponse> FetchZeroRatingToken(string reason = "token_expired")
        {
            var request = new RequestBuilder(Account).SetUrl("zr/token/result/")
                                                     .SetNeedsAuth(false)
                                                     .AddParam("custom_device_id", Account.AccountInfo.Uuid)
                                                     .AddParam("device_id", Account.AccountInfo.DeviceId)
                                                     .AddParam("fetch_reason", reason)
                                                     .AddParam("token_hash", Account.LoginClient.LoginInfo.ZrToken);

            var response = await Account.ApiRequest<TokenResultResponse>(request)
                                        .ConfigureAwait(false);

            await SaveZeroRatingToken(response.Token)
                .ConfigureAwait(false);

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

            await Account.Storage.LoginInfo.SaveAsync(Account.LoginClient.LoginInfo)
                         .ConfigureAwait(false);
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
                    if (param.Name.ValueKind != JsonValueKind.String && param.Value.ValueKind != JsonValueKind.String)
                    {
                        continue;
                    }

                    experiments[group][param.Name.GetString()] = param.Value.GetString();
                }
            }

            Account.LoginClient.LoginInfo.Experiments = experiments;
            Account.LoginClient.LoginInfo.LastExperiments.Update();

            await Account.Storage.LoginInfo.SaveAsync(Account.LoginClient.LoginInfo)
                         .ConfigureAwait(false);
        }

        public async Task<FetchQpDataResponse> GetQpFetch()
        {
            const string query = "viewer() {eligible_promotions.surface_nux_id(<surface>).external_gating_permitted_qps(<external_gating_permitted_qps>).supports_client_filters(true) {edges {priority,time_range {start,end},node {id,promotion_id,max_impressions,triggers,contextual_filters {clause_type,filters {filter_type,unknown_action,value {name,required,bool_value,int_value, string_value},extra_datas {name,required,bool_value,int_value, string_value}},clauses {clause_type,filters {filter_type,unknown_action,value {name,required,bool_value,int_value, string_value},extra_datas {name,required,bool_value,int_value, string_value}},clauses {clause_type,filters {filter_type,unknown_action,value {name,required,bool_value,int_value, string_value},extra_datas {name,required,bool_value,int_value, string_value}},clauses {clause_type,filters {filter_type,unknown_action,value {name,required,bool_value,int_value, string_value},extra_datas {name,required,bool_value,int_value, string_value}}}}}},template {name,parameters {name,required,bool_value,string_value,color_value,}},creatives {title {text},content {text},footer {text},social_context {text},primary_action{title {text},url,limit,dismiss_promotion},secondary_action{title {text},url,limit,dismiss_promotion},dismiss_action{title {text},url,limit,dismiss_promotion},image.scale(<scale>) {uri,width,height}}}}}}";
            var surfacesToQueries = JsonSerializer.Serialize(new Dictionary<string, string>
                                                             {
                                                                 {
                                                                     Constants.Request.Instance.SurfaceParams[0], query
                                                                 },
                                                                 {
                                                                     Constants.Request.Instance.SurfaceParams[1], query
                                                                 }
                                                             }, JsonSerializerDefaultOptions.Instance);

            var request = new RequestBuilder(Account).SetUrl("qp/batch_fetch/")
                                                     .AddPost("vc_policy", "default")
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("surfaces_to_queries", surfacesToQueries)
                                                     .AddPost("version", 1)
                                                     .AddPost("scale", 2);

            return await Account.ApiRequest<FetchQpDataResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<FacebookOtaResponse> GetFacebookOta()
        {
            var request = new RequestBuilder(Account).SetUrl("facebook_ota/")
                                                     .AddParam("fields", Facebook.Instance.FacebookOtaFields)
                                                     .AddParam("custom_user_id", Account.AccountInfo.AccountId)
                                                     .AddParam("signed_body", $"{Signer.GenerateSignature(string.Empty)}.")
                                                     .AddParam("ig_sig_key_version", Version.Instance.SigningKeyVersion)
                                                     .AddParam("version_code", Version.Instance.VersionCode)
                                                     .AddParam("version_name", Version.Instance.InstagramVersion)
                                                     .AddParam("custom_app_id", Facebook.Instance.FacebookOrcaApplicationId)
                                                     .AddParam("custom_device_id", Account.AccountInfo.Uuid);

            return await Account.ApiRequest<FacebookOtaResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<GenericResponse> LogResurrectAttribution()
        {
            var request = new RequestBuilder(Account).SetUrl("attribution/log_resurrect_attribution/")
                                                     .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                     .AddPost("_uid", Account.AccountInfo.AccountId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

            try
            {
                return await Account.ApiRequest<GenericResponse>(request)
                                    .ConfigureAwait(false);
            }
            catch
            {
                return null;
            }
        }

        public async Task<QpCooldownsResponse> GetQpCooldowns()
        {
            var request = new RequestBuilder(Account).SetUrl("qp/get_cooldowns/")
                                                     .AddParam("signed_body", Signer.GenerateSignature("{}") + ".{}")
                                                     .AddParam("ig_sig_key_version", Version.Instance.SigningKeyVersion);

            return await Account.ApiRequest<QpCooldownsResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<ArlinkDownloadInfoResponse> GetArlinkDownloadInfo()
        {
            var request = new RequestBuilder(Account).SetUrl("users/arlink_download_info/")
                                                     .AddParam("version_override", "2.2.1");

            return await Account.ApiRequest<ArlinkDownloadInfoResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<MsisdnHeaderResponse> BootstrapMsisdnHeader(string usage = "ig_select_app")
        {
            var request = new RequestBuilder(Account).SetUrl("accounts/msisdn_header_bootstrap/")
                                                     .SetNeedsAuth(false)
                                                     .AddPost("mobile_subno_usage", usage)
                                                     .AddPost("device_id", Account.AccountInfo.Uuid);

            return await Account.ApiRequest<MsisdnHeaderResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}