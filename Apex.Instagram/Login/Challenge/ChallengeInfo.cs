using System;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Response.JsonMap;
using Apex.Instagram.Utils;

using JetBrains.Annotations;

using MessagePack;

namespace Apex.Instagram.Login.Challenge
{
    [MessagePackObject]
    internal class ChallengeInfo
    {
        [UsedImplicitly]
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

//            if ( response.Challenge.ApiPath[0] == '/' )
//            {
            response.Challenge.ApiPath = response.Challenge.ApiPath.Remove(0, 1);
            ApiPath                    = new Uri(response.Challenge.ApiPath, UriKind.Relative);
//            }

            LogOut = response.Challenge.Logout != null && (bool) response.Challenge.Logout;
        }

        [Key(0)]
        public Uri ApiPath { get; set; }

        [Key(1)]
        public bool LogOut { get; set; }

        [IgnoreMember]
        public Uri ResetUrl
        {
            get
            {
                if ( ApiPath == null )
                {
                    throw new ChallengeException("No url found.");
                }

                var builder = new UrlBuilder(ApiPath).SetRelativeUri()
                                                     .AddSegmentAfter(@"reset/", @"challenge/");

                return builder.Build();
            }
        }

        [IgnoreMember]
        public Uri ReplayUrl
        {
            get
            {
                if ( ApiPath == null )
                {
                    throw new ChallengeException("No url found.");
                }

                var builder = new UrlBuilder(ApiPath).SetRelativeUri()
                                                     .AddSegmentAfter(@"replay/", @"challenge/");

                return builder.Build();
            }
        }
    }
}