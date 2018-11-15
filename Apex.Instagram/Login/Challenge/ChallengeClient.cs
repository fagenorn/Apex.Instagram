using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Request;
using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Login.Challenge
{
    public class ChallengeClient
    {
        public async Task Reset()
        {
            ThrowIfUnavailable();

            await ResetChallenge()
                .ConfigureAwait(false);
        }

        public StepInfo GetNextStep()
        {
            ThrowIfUnavailable();

            if ( _challengeResponse == null )
            {
                throw new ChallengeException("No challenge response information available.");
            }

            switch ( _challengeResponse.StepName )
            {
                case "submit_phone":
                    _stepInfo = new StepPhoneInfo(_account, _challengeResponse.StepData, ChallengeInfo);

                    break;
                case "verify_code":
                    _stepInfo = new StepVerifySmsInfo(_account, _challengeResponse.StepData, ChallengeInfo);

                    break;
                default:

                    throw new ChallengeException("Step name {0} is unknown.", _challengeResponse.StepName);
            }

            return _stepInfo;
        }

        public async Task DoNextStep(string input)
        {
            ThrowIfUnavailable();

            if ( _stepInfo == null )
            {
                throw new ChallengeException("No step information available.");
            }

            var response = await _stepInfo.Submit(input);
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

        private StepInfo _stepInfo;

        #endregion

        #region Properties

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