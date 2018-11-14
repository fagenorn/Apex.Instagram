using System.Threading.Tasks;

using Apex.Instagram.Login.Exception;

namespace Apex.Instagram.Login.Challenge
{
    public class ChallengeClient
    {
        public void Reset()
        {
            if ( !HasChallenge )
            {
                throw new NoChallengeException();
            }

            // _account.blabla.ResetChallenge(ChallengeInfo.ResetUrl);
        }

        #region Fields

        private readonly Account _account;

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