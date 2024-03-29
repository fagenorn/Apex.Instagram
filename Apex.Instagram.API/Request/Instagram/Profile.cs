﻿using System.Threading.Tasks;

using Apex.Instagram.API.Request.Exception;
using Apex.Instagram.API.Response.JsonMap;

namespace Apex.Instagram.API.Request.Instagram
{
    internal class Profile : RequestCollection
    {
        public Profile(Account account) : base(account) { }

        public async Task<GenericResponse> SetContactPointPrefill(string usage)
        {
            var request = new RequestBuilder(Account).SetUrl("accounts/contact_point_prefill/")
                                                     .SetNeedsAuth(false)
                                                     .AddPost("phone_id", Account.AccountInfo.PhoneId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("usage", usage);

            return await Account.ApiRequest<GenericResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<MultipleAccountFamilyResponse> GetAccountFamily()
        {
            var request = new RequestBuilder(Account).SetUrl("multiple_accounts/get_account_family/");

            return await Account.ApiRequest<MultipleAccountFamilyResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<FacebookIdResponse> GetFacebookId()
        {
            try
            {
                var request = new RequestBuilder(Account).SetUrl("fb/get_connected_fbid/")
                                                         .SetSignedPost(false)
                                                         .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                         .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

                return await Account.ApiRequest<FacebookIdResponse>(request)
                                    .ConfigureAwait(false);
            }
            catch
            {
                return null;
            }
        }

        public async Task<LinkageStatusResponse> GetLinkageStatus()
        {
            var request = new RequestBuilder(Account).SetUrl("linked_accounts/get_linkage_status/");

            return await Account.ApiRequest<LinkageStatusResponse>(request)
                                .ConfigureAwait(false);
        }

        public async Task<GenericResponse> GetProcessContactPointSignals()
        {
            try
            {
                var request = new RequestBuilder(Account).SetUrl("accounts/process_contact_point_signals/")
                                                         .AddPost("google_tokens", "[]")
                                                         .AddPost("phone_id", Account.AccountInfo.PhoneId)
                                                         .AddPost("_uid", Account.AccountInfo.AccountId)
                                                         .AddPost("device_id", Account.AccountInfo.DeviceId)
                                                         .AddPost("_uuid", Account.AccountInfo.Uuid)
                                                         .AddPost("_csrftoken", Account.LoginClient.CsrfToken);

                return await Account.ApiRequest<GenericResponse>(request)
                                    .ConfigureAwait(false);
            }
            catch (ThrottledException)
            {
                return null;
            }
        }

        public async Task<PrefillCandidatesResponse> GetPrefillCandidates()
        {
            var request = new RequestBuilder(Account).SetUrl("accounts/get_prefill_candidates/")
                                                     .SetNeedsAuth(false)
                                                     .AddPost("android_device_id", Account.AccountInfo.DeviceId)
                                                     .AddPost("phone_id", Account.AccountInfo.PhoneId)
                                                     .AddPost("device_id", Account.AccountInfo.Uuid)
                                                     .AddPost("usages", "[\"account_recovery_omnibox\"]");

            return await Account.ApiRequest<PrefillCandidatesResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}