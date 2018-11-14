using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;

namespace Apex.Instagram.Login.Challenge
{
    public class ChallengeClient
    {
        #region Fields

        private readonly Account _account;

        #endregion

        public void Reset()
        {
            if ( !HasChallenge )
            {
                throw new NoChallengeException();
            }

            // _account.blabla.ResetChallenge(ChallengeInfo.ResetUrl);
        }

//        internal async Task<ChallengeResponse> ResetChallenge()
//        {
//            var request = new RequestBuilder(_account).SetNeedsAuth(false)
//                                                      .SetUrl(ChallengeInfo.ResetUrl)
//                                                      .AddPost("_csrftoken", _account.LoginClient.CsrfToken);
//
//            return await _account.ApiRequest<ChallengeResponse>(request.Build)
//                                 .ConfigureAwait(false);
//        }

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