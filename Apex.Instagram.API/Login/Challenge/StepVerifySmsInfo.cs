﻿using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Login.Challenge
{
    internal class StepVerifySmsInfo : StepVerifyInfo
    {
        public StepVerifySmsInfo(Account account, StepData stepData, ChallengeInfo challengeInfo) : base(account, stepData, challengeInfo) { }

        public override string Title => "Enter the code.";

        public override string Description => $"Enter the 6 digit code that was sent to your mobile: {StepData.PhoneNumber}.\r\n";
    }
}