using System;

using Apex.Instagram.Login.Exception;
using Apex.Instagram.Response.JsonMap;

using MessagePack;

namespace Apex.Instagram.Login.Challenge
{
    [MessagePackObject]
    internal class ChallengeInfo
    {
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

            Url    = response.Challenge.Url;
            LogOut = response.Challenge.Logout != null && (bool) response.Challenge.Logout;
        }

        [Key(0)]
        public Uri Url { get; set; }

        [Key(1)]
        public bool LogOut { get; set; }

        [Key(2)]
        public string StepData { get; set; }

        [Key(3)]
        public string StepName { get; set; }

        [Key(4)]
        public string Action { get; set; }

        [IgnoreMember]
        public Uri ResetUrl
        {
            get
            {
                if ( Url == null )
                {
                    throw new ChallengeException("No url found.");
                }

                var builder = new UriBuilder(Url);
                builder.Path = builder.Path.Replace(@"challenge/", @"challenge/reset/");

                return builder.Uri;
            }
        }
    }
}