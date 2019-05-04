using System;
using System.Threading.Tasks;

using Apex.Instagram.Request;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Login.Challenge
{
    internal abstract class StepSelectInfo : StepInfo
    {
        public StepSelectInfo(Account account, StepData stepData, ChallengeInfo challengeInfo) : base(account, stepData, challengeInfo) { }

        public abstract override string Title { get; }

        public abstract override string Description { get; }

        private protected override async Task<ChallengeResponse> SubmitInternalAsync(Uri url, string input)
        {
            var request = new RequestBuilder(Account).SetNeedsAuth(false)
                                                     .SetUrl(url)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("choice", input);

            return await Account.ApiRequest<ChallengeResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}