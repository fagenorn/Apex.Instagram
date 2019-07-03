using Apex.Instagram.API.Login.Challenge;
using Apex.Instagram.API.Response.JsonMap;

namespace Apex.Instagram.API.Request.Exception
{
    /// <inheritdoc />
    /// <summary>
    ///     Challenge required exception.
    /// </summary>
    public class ChallengeRequiredException : RequestException
    {
        private Response.JsonMap.Response _response;

        internal ChallengeInfo Challenge { get; private set; }

        internal override Response.JsonMap.Response Response
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