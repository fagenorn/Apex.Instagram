using System;
using System.Threading.Tasks;

using Apex.Instagram.Request;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Login.Challenge
{
    /// <inheritdoc />
    /// <summary>Submit phone challenge step information.</summary>
    internal sealed class StepPhoneInfo : StepInfo
    {
        internal StepPhoneInfo(Account account, StepData stepData, ChallengeInfo challengeInfo) : base(account, stepData, challengeInfo) { }

        /// <inheritdoc />
        public override string Title => "Submit a phone number.";

        /// <inheritdoc />
        public override string Description => $"Enter a valid phone number.\nCurrent phone number: {StepData.PhoneNumber}.";

        private protected override async Task<ChallengeResponse> SubmitInternalAsync(Uri url, string input)
        {
            var request = new RequestBuilder(Account).SetNeedsAuth(false)
                                                     .SetUrl(url)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("phone_number", input);

            return await Account.ApiRequest<ChallengeResponse>(request.Build)
                                .ConfigureAwait(false);
        }
    }
}