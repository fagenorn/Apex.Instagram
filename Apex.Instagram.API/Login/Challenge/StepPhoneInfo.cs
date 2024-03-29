﻿using System;
using System.Threading.Tasks;

using Apex.Instagram.API.Request;
using Apex.Instagram.API.Response.JsonMap;
using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Login.Challenge
{
    /// <inheritdoc />
    /// <summary>Submit phone challenge step information.</summary>
    internal sealed class StepPhoneInfo : StepInfo
    {
        internal StepPhoneInfo(Account account, StepData stepData, ChallengeInfo challengeInfo) : base(account, stepData, challengeInfo) { }

        /// <inheritdoc />
        public override string Title => "Submit a phone number.";

        /// <inheritdoc />
        public override string Description => $"Enter a valid phone number.\r\nCurrent phone number: {StepData.PhoneNumber}.\r\n";

        private protected override async Task<Response.JsonMap.Response> SubmitInternalAsync(Uri url, string input)
        {
            var request = new RequestBuilder(Account).SetNeedsAuth(false)
                                                     .SetUrl(url)
                                                     .AddPost("guid", Account.AccountInfo.Uuid)
                                                     .AddPost("device_id", Account.AccountInfo.DeviceId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("phone_number", input);

            return await Account.ApiRequest<CheckpointResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}