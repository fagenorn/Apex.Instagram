﻿using System;
using System.Linq;
using System.Text;

using Apex.Shared.Model;

namespace Apex.Instagram.API.Utils
{
    internal class Instagram
    {
        public string GenerateMultipartBoundary()
        {
            const string chars  = "-_1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const int    length = 30;
            var          result = new StringBuilder();
            var          max    = chars.Length;
            for ( var i = 0; i < length; i++ )
            {
                result.Append(chars[Randomizer.Instance.Number(max)]);
            }

            return result.ToString();
        }

        public string GenerateUuid(bool keepDashes = true)
        {
            var uuidSplit = Guid.NewGuid()
                                .ToString()
                                .Split('-');

            uuidSplit[1] = uuidSplit[1][0] + "663";

            return string.Join(keepDashes ? "-" : string.Empty, uuidSplit);
        }

        public string GenerateDeviceId()
        {
            string[] securityIds = {"9774d56d682e549c", "9d1d1f0dfa440886", "fc067667235b8f19"};

            string hash;
            do
            {
                hash = Hashing.Instance.ByteToString(Hashing.Instance.Md5(Epoch.Current.ToString("F7")
                                                                               .Replace('.', '\n')));
            } while ( securityIds.Contains(hash) );

            return "android-" + hash.Substring(0, 16);
        }

        #region Singleton     

        private static Instagram _instance;

        private static readonly object Lock = new object();

        private Instagram() { }

        public static Instagram Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Instagram());
                }
            }
        }

        #endregion
    }
}