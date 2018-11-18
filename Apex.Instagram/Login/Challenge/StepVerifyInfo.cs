using System;
using System.Threading.Tasks;

using Apex.Instagram.Request;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Login.Challenge
{
    /// <inheritdoc />
    /// <summary>Submit security code challenge step information.</summary>
    public abstract class StepVerifyInfo : StepInfo
    {
        internal StepVerifyInfo(Account account, StepData stepData, ChallengeInfo challengeInfo) : base(account, stepData, challengeInfo) { }

        /// <inheritdoc />
        public abstract override string Title { get; }

        /// <inheritdoc />
        public abstract override string Description { get; }

        private protected override async Task<ChallengeResponse> SubmitInternalAsync(Uri url, string input)
        {
            var request = new RequestBuilder(Account).SetNeedsAuth(false)
                                                     .SetUrl(url)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("security_code", input);

            return await Account.ApiRequest<ChallengeResponse>(request.Build)
                                .ConfigureAwait(false);
        }
    }
}