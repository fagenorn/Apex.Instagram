﻿using System.Collections.Generic;

using Apex.Instagram.Request.Exception;

namespace Apex.Instagram.Constants
{
    internal class Response
    {
        public List<IExceptionMap> ExceptionMap = new List<IExceptionMap>
                                                  {
                                                      new ExceptionMap<LoginRequiredException>(@"login_required"),
                                                      new ExceptionMap<CheckpointRequiredException>(@"checkpoint_required", @"checkpoint_challenge_required"),
                                                      new ExceptionMap<ChallengeRequiredException>(@"challenge_required"),
                                                      new ExceptionMap<FeedbackRequiredException>(@"feedback_required"),
                                                      new ExceptionMap<ConsentRequiredException>(@"consent_required"),
                                                      new ExceptionMap<IncorrectPasswordException>(@"The password you entered is incorrect. Please try again.", @"bad_password"),
                                                      new ExceptionMap<InvalidSmsCodeException>(@"Please check the security code we sent you and try again.", @"sms_code_validation_code_invalid"),
                                                      new ExceptionMap<AccountDisabledException>(@"Your account has been disabled for violating our terms. Learn how you may be able to restore your account."),
                                                      new ExceptionMap<SentryBlockException>(@"sentry_block"),
                                                      new ExceptionMap<InvalidUserException>(@"The username you entered doesn't appear to belong to an account. Please check your username and try again.", @"invalid_user"),
                                                      new ExceptionMap<ForcedPasswordResetException>(@"To secure your account, we've reset your password. Tap ""Get help signing in"" on the login screen and follow the instructions to access your account.")
                                                  };

        public string StatusOk { get; } = "ok";

        public string StatusFail { get; } = "fail";

        #region Singleton     

        private static Response _instance;

        private static readonly object Lock = new object();

        private Response() { }

        public static Response Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Response());
                }
            }
        }

        #endregion
    }
}