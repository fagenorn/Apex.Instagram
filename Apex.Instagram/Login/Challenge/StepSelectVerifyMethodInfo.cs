using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Login.Challenge
{
    internal class StepSelectVerifyMethodInfo : StepSelectInfo
    {
        public StepSelectVerifyMethodInfo(Account account, StepData stepData, ChallengeInfo challengeInfo) : base(account, stepData, challengeInfo) { }

        public override string Title => "Select a verification method.";

        /// <summary>Phone = 0, Email = 1</summary>
        public override string Description => $"Select mobile or email verification. (Phone = 0, Email = 1)\n\rEmail: {StepData.Email ?? "N/A"}\n\rPhone: {StepData.PhoneNumber ?? "N/A"}";
    }
}