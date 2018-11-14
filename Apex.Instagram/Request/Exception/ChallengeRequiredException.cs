using Apex.Instagram.Login.Challenge;
using Apex.Instagram.Response.JsonMap;

namespace Apex.Instagram.Request.Exception
{
    public class ChallengeRequiredException : RequestException
    {
        private Response.JsonMap.Response _response;

        internal ChallengeInfo Challenge { get; private set; }

        public override Response.JsonMap.Response Response
        {
            get => _response;
            set
            {
                _response = value;
                if ( _response is LoginResponse s )
                {
                    Challenge = new ChallengeInfo(s);
                }
            }
        }
    }
}