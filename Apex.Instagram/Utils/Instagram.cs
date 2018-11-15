using System;
using System.Text;

namespace Apex.Instagram.Utils
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
            var guid = Guid.NewGuid()
                           .ToString();

            return keepDashes ? guid : guid.Replace('-', '\0');
        }

        public string GenerateDeviceId()
        {
            return "android-" + Hashing.Instance.ByteToString(Hashing.Instance.Md5(GenerateUuid()))
                                       .Substring(0, 16);
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