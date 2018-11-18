using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Request;
using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Login.Challenge
{
    /// <summary>Client to view and solve Instagram challenges.</summary>
    public class ChallengeClient
    {
        /// <summary>Resets the challenge.</summary>
        public async Task Reset()
        {
            ThrowIfUnavailable();

            await ResetChallenge()
                .ConfigureAwait(false);
        }

        /// <summary>Resend the verification code for the current instance.</summary>
        /// <exception cref="ChallengeException">Replay challenge not allowed for the current challenge step.</exception>
        public async Task Replay()
        {
            ThrowIfUnavailable();

            if ( !(_stepInfo is StepVerifyInfo) )
            {
                throw new ChallengeException("Replay challenge not allowed for the current challenge step.");
            }

            var response = await _previousStepInfo.Replay()
                                                  .ConfigureAwait(false);

            CheckIfCompleted(response);
        }

        /// <summary>Gets the information regarding the next challenge step.</summary>
        /// <returns>
        ///     <see cref="StepInfo" />
        /// </returns>
        /// <exception cref="ChallengeException">No challenge response information available.</exception>
        /// <exception cref="ChallengeException">Step name is unknown.</exception>
        public StepInfo GetNextStep()
        {
            ThrowIfUnavailable();

            if ( _challengeResponse == null )
            {
                throw new ChallengeException("No challenge response information available.");
            }

            _previousStepInfo = _stepInfo;

            switch ( _challengeResponse.StepName )
            {
                case @"submit_phone":
                    _stepInfo = new StepPhoneInfo(_account, _challengeResponse.StepData, ChallengeInfo);

                    break;
                case @"verify_code":
                    _stepInfo = new StepVerifySmsInfo(_account, _challengeResponse.StepData, ChallengeInfo);

                    break;
                default:

                    throw new ChallengeException("Step name {0} is unknown.", _challengeResponse.StepName);
            }

            return _stepInfo;
        }

        /// <summary>  Performs the next challenge step with the provided input.</summary>
        /// <param name="input">The challenge input.</param>
        /// <exception cref="ChallengeException">No step information available.</exception>
        public async Task DoNextStep(string input)
        {
            ThrowIfUnavailable();

            if ( _stepInfo == null )
            {
                throw new ChallengeException("No step information available.");
            }

            var response = await _stepInfo.Submit(input)
                                          .ConfigureAwait(false);

            CheckIfCompleted(response);
        }

        #region Private methods

        private async Task ResetChallenge()
        {
            var request = new RequestBuilder(_account).SetNeedsAuth(false)
                                                      .SetUrl(ChallengeInfo.ResetUrl)
                                                      .AddPost("_csrftoken", _account.LoginClient.CsrfToken);

            var response = await _account.ApiRequest<ChallengeResponse>(request.Build)
                                         .ConfigureAwait(false);

            CheckIfCompleted(response);
        }

        private void ThrowIfUnavailable()
        {
            if ( !HasChallenge )
            {
                throw new ChallengeException("No challenge available.");
            }

            if ( Completed )
            {
                throw new ChallengeException("Challenge has been completed.");
            }
        }

        private void CheckIfCompleted(ChallengeResponse response)
        {
            _challengeResponse = response;
            Completed          = response.Action != null && response.Action == "close";
        }

        #endregion

        #region Fields

        private readonly Account _account;

        private ChallengeResponse _challengeResponse;

        private StepInfo _previousStepInfo;

        private StepInfo _stepInfo;

        #endregion

        #region Properties

        /// <summary>Gets a value indicating whether this <see cref="ChallengeClient" /> is completed.</summary>
        /// <value>
        ///     <c>true</c> if completed; otherwise, <c>false</c>.
        /// </value>
        public bool Completed { get; private set; }

        internal ChallengeInfo ChallengeInfo { get; private set; }

        internal bool HasChallenge => ChallengeInfo != null;

        #endregion

        #region Constructor

        private ChallengeClient(Account account) { _account = account; }

        private async Task<ChallengeClient> InitializeAsync()
        {
            ChallengeInfo = await _account.Storage.ChallengeInfo.LoadAsync()
                                          .ConfigureAwait(false);

            return this;
        }

        internal static Task<ChallengeClient> CreateAsync(Account account)
        {
            var ret = new ChallengeClient(account);

            return ret.InitializeAsync();
        }

        #endregion
    }
}