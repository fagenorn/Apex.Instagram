using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Request.Exception.EndpointException;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Login.Challenge
{
    public abstract class StepInfo
    {
        internal readonly Account Account;

        internal readonly ChallengeInfo ChallengeInfo;

        internal readonly StepData StepData;

        private bool _done;

        internal StepInfo(Account account, StepData stepData, ChallengeInfo challengeInfo)
        {
            Account       = account;
            StepData      = stepData;
            ChallengeInfo = challengeInfo;
        }

        public abstract string Title { get; }

        public abstract string Description { get; }

        internal async Task<ChallengeResponse> Submit(string input)
        {
            if ( _done )
            {
                throw new ChallengeException("Step has already been completed.");
            }

            try
            {
                var result = await SubmitInternalAsync(input);
                _done = true;

                return result;
            }
            catch (EndpointException e)
            {
                Account.Logger.Warning<StepInfo>(e, "Failed to perform challenge step. Step input: {0}", input);

                throw new ChallengeException("Failed to perform challenge step.", e);
            }
        }

        internal abstract Task<ChallengeResponse> SubmitInternalAsync(string input);
    }
}