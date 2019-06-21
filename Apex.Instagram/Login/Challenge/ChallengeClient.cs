using System;
using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Request;
using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Login.Challenge
{
    /// <summary>Client to view and solve Instagram challenges.</summary>
    public class ChallengeClient
    {
        /// <summary>Resets the challenge and gets the information regarding the next challenge step.</summary>
        /// <returns>
        ///     <see cref="StepInfo" />
        /// </returns>
        /// <exception cref="ChallengeException">No challenge response information available.</exception>
        [Obsolete]
        public async Task<StepInfo> Reset()
        {
            ThrowIfUnavailable();

            await ResetChallenge()
                .ConfigureAwait(false);

            return GetNextStep();
        }

        public async Task<StepInfo> Start()
        {
            ThrowIfUnavailable();

            await StartChallenge()
                .ConfigureAwait(false);

            return GetNextStep();
        }

        /// <summary>Resend the verification code for the current instance.</summary>
        /// <exception cref="ChallengeException">Replay challenge not allowed for the current challenge step.</exception>
        public async Task Replay()
        {
            ThrowIfUnavailable();

            if ( !(_stepInfo is StepVerifyInfo) || _previousStepInfo == null )
            {
                throw new ChallengeException("Replay challenge not allowed for the current challenge step.");
            }

            var response = (CheckpointResponse) await _previousStepInfo.Replay()
                                                                       .ConfigureAwait(false);

            _checkpointResponse = response;
        }

        /// <summary>
        ///     Performs the next challenge step with the provided input and gets the information regarding the next
        ///     challenge step.
        /// </summary>
        /// <param name="input">The challenge input.</param>
        /// <returns>
        ///     <see cref="StepInfo" />
        /// </returns>
        /// <exception cref="ChallengeException">No step information available.</exception>
        /// <exception cref="ChallengeException">No challenge response information available.</exception>
        public async Task<StepInfo> DoNextStep(string input)
        {
            ThrowIfUnavailable();

            if ( _stepInfo == null )
            {
                throw new ChallengeException("No step information available.");
            }

            var response = await _stepInfo.Submit(input)
                                          .ConfigureAwait(false);

            if ( _stepInfo is StepVerifyInfo && response is LoginResponse loginResponse )
            {
                await FinishCheckpoint(loginResponse);

                return null;
            }

            _checkpointResponse = (CheckpointResponse) response;

            return GetNextStep();
        }

        #region Private methods

        private StepInfo GetNextStep()
        {
            ThrowIfUnavailable();

            if ( _checkpointResponse == null )
            {
                throw new ChallengeException("No challenge response information available.");
            }

            _previousStepInfo = _stepInfo;

            switch ( _checkpointResponse.StepName )
            {
                case @"submit_phone":
                    _stepInfo = new StepPhoneInfo(_account, _checkpointResponse.StepData, ChallengeInfo);

                    break;
                case @"verify_code":
                    _stepInfo = new StepVerifySmsInfo(_account, _checkpointResponse.StepData, ChallengeInfo);

                    break;
                case @"select_verify_method":
                    _stepInfo = new StepSelectVerifyMethodInfo(_account, _checkpointResponse.StepData, ChallengeInfo);

                    break;
                default:

                    throw new ChallengeException("Step name {0} is unknown.", _checkpointResponse.StepName);
            }

            return _stepInfo;
        }

        private async Task ResetChallenge()
        {
            var request = new RequestBuilder(_account).SetNeedsAuth(false)
                                                      .SetUrl(ChallengeInfo.ResetUrl)
                                                      .AddPost("_csrftoken", _account.LoginClient.CsrfToken);

            var response = await _account.ApiRequest<CheckpointResponse>(request)
                                         .ConfigureAwait(false);

            _checkpointResponse = response;
        }

        private async Task StartChallenge()
        {
            var request = new RequestBuilder(_account).SetNeedsAuth(false)
                                                      .SetSignedPost(false)
                                                      .SetUrl(ChallengeInfo.ApiPath)
                                                      .AddParam("guid", _account.AccountInfo.Uuid)
                                                      .AddParam("device_id", _account.AccountInfo.DeviceId);

            var response = await _account.ApiRequest<CheckpointResponse>(request)
                                         .ConfigureAwait(false);

            _checkpointResponse = response;
        }

        private void ThrowIfUnavailable()
        {
            if ( !HasChallenge )
            {
                throw new ChallengeException("No challenge available.");
            }

//            if ( Completed )
//            {
//                throw new ChallengeException("Challenge has been completed.");
//            }
        }

        private async Task FinishCheckpoint(LoginResponse response)
        {
            await _account.LoginClient.UpdateLoginState(response)
                          .ConfigureAwait(false);

            await _account.LoginClient.LoginFlow(true)
                          .ConfigureAwait(false);
        }

        #endregion

        #region Fields

        private readonly Account _account;

        private CheckpointResponse _checkpointResponse;

        private StepInfo _previousStepInfo;

        private StepInfo _stepInfo;

        #endregion

        #region Properties

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