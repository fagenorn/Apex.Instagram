using System;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Utils;

using MessagePack;

namespace Apex.Instagram.Login.Challenge
{
    [MessagePackObject]
    internal class ChallengeInfo
    {
        public ChallengeInfo() { }

        public ChallengeInfo(LoginResponse response)
        {
            if ( response == null )
            {
                throw new ChallengeException("Response is empty.");
            }

            if ( response.Challenge == null )
            {
                throw new ChallengeException("No info found.");
            }

            if ( response.Challenge.ApiPath[0] == '/' )
            {
                response.Challenge.ApiPath = response.Challenge.ApiPath.Remove(0, 1);
            }

            var baseUri = Constants.Request.Instance.ApiUrl[1];
            Url = new UrlBuilder(baseUri).AddSegments(response.Challenge.ApiPath)
                                         .Build();

            LogOut = response.Challenge.Logout != null && (bool) response.Challenge.Logout;
        }

        [Key(0)]
        public Uri Url { get; set; }

        [Key(1)]
        public bool LogOut { get; set; }

        [IgnoreMember]
        public Uri ResetUrl
        {
            get
            {
                if ( Url == null )
                {
                    throw new ChallengeException("No url found.");
                }

                var builder = new UrlBuilder(Url);
                builder.AddSegmentAfter(@"reset/", @"challenge/");

                return builder.Build();
            }
        }

        [IgnoreMember]
        public Uri ReplayUrl
        {
            get
            {
                if ( Url == null )
                {
                    throw new ChallengeException("No url found.");
                }

                var builder = new UrlBuilder(Url);
                builder.AddSegmentAfter(@"replay/", @"challenge/");

                return builder.Build();
            }
        }
    }
}