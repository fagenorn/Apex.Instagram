using System.Threading.Tasks;

using Apex.Instagram.Request;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Login.Challenge
{
    public sealed class StepPhoneInfo : StepInfo
    {
        internal StepPhoneInfo(Account account, StepData stepData, ChallengeInfo challengeInfo) : base(account, stepData, challengeInfo) { }

        public override string Title => "Submit a phone number.";

        public override string Description => $"Enter a valid phone number.\nCurrent phone number: {StepData.PhoneNumber}.";

        protected override async Task<ChallengeResponse> SubmitInternalAsync(string input)
        {
            var request = new RequestBuilder(Account).SetNeedsAuth(false)
                                                     .SetUrl(ChallengeInfo.Url)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("phone_number", input);

            return await Account.ApiRequest<ChallengeResponse>(request.Build)
                                .ConfigureAwait(false);
        }
    }
}