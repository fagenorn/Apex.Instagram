using System;
using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Request.Exception.EndpointException;
using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Login.Challenge
{
    /// <summary>Information regarding a challenge step.</summary>
    public abstract class StepInfo
    {
        private readonly ChallengeInfo _challengeInfo;

        internal readonly Account Account;

        internal readonly StepData StepData;

        private bool _done;

        private string _replayInput;

        internal StepInfo(Account account, StepData stepData, ChallengeInfo challengeInfo)
        {
            Account        = account;
            StepData       = stepData;
            _challengeInfo = challengeInfo;
        }

        /// <summary>Gets the title of the challenge step.</summary>
        /// <value>The title.</value>
        public abstract string Title { get; }

        /// <summary>Gets the description of the challenge step.</summary>
        /// <value>The description.</value>
        public abstract string Description { get; }

        internal async Task<Response.JsonMap.Response> Submit(string input)
        {
            if ( _done )
            {
                throw new ChallengeException("Step has already been completed.");
            }

            try
            {
                var result = await SubmitInternalAsync(_challengeInfo.ApiPath, input)
                                 .ConfigureAwait(false);

                _done        = true;
                _replayInput = input;

                return result;
            }
            catch (EndpointException e)
            {
                Account.Logger.Warning<StepInfo>(e, "Failed to perform challenge step. Step input: {0}", input);

                throw new ChallengeException("Failed to perform challenge step.", e);
            }
        }

        internal async Task<Response.JsonMap.Response> Replay()
        {
            if ( !_done )
            {
                throw new ChallengeException("Can't replay step that hasn't been completed yet.");
            }

            try
            {
                var result = await SubmitInternalAsync(_challengeInfo.ReplayUrl, _replayInput)
                                 .ConfigureAwait(false);

                return result;
            }
            catch (EndpointException e)
            {
                Account.Logger.Warning<StepInfo>(e, "Failed to perform challenge step. Step input: {0}", _replayInput);

                throw new ChallengeException("Failed to perform challenge step.", e);
            }
        }

        private protected abstract Task<Response.JsonMap.Response> SubmitInternalAsync(Uri url, string input);
    }
}