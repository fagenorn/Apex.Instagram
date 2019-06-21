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

        private protected override async Task<Response.JsonMap.Response> SubmitInternalAsync(Uri url, string input)
        {
            var request = new RequestBuilder(Account).SetNeedsAuth(false)
                                                     .SetUrl(url)
                                                     .AddPost("guid", Account.AccountInfo.Uuid)
                                                     .AddPost("device_id", Account.AccountInfo.DeviceId)
                                                     .AddPost("_csrftoken", Account.LoginClient.CsrfToken)
                                                     .AddPost("choice", input);

            return await Account.ApiRequest<CheckpointResponse>(request)
                                .ConfigureAwait(false);
        }
    }
}