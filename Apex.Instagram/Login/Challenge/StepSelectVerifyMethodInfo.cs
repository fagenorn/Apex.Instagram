using System.Text;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Login.Challenge
{
    internal class StepSelectVerifyMethodInfo : StepSelectInfo
    {
        public StepSelectVerifyMethodInfo(Account account, StepData stepData, ChallengeInfo challengeInfo) : base(account, stepData, challengeInfo) { }

        public override string Title => "Select a verification method.";

        /// <summary>Phone = 0, Email = 1</summary>
        public override string Description
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("Select verification method option:");

                if (!string.IsNullOrWhiteSpace(StepData.PhoneNumber))
                {
                    sb.AppendLine($"0: Phone ({StepData.PhoneNumber})");
                }

                if (!string.IsNullOrWhiteSpace(StepData.Email))
                {
                    sb.AppendLine($"1: Email ({StepData.Email})");
                }

                return sb.ToString();
            }
        }
    }
}