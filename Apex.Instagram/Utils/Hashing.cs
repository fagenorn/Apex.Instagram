using System;
using System.Security.Cryptography;
using System.Text;

namespace Apex.Instagram.Utils
{
    internal class Hashing
    {
        public byte[] Md5(string value)
        {
            using (var md5 = MD5.Create())
            {
                var encodedString = Encoding.UTF8.GetBytes(value);
                md5.ComputeHash(encodedString);

                return md5.Hash;
            }
        }

        public byte[] Sha256(string value, byte[] key = null)
        {
            using (var hma = key == null ? new HMACSHA256() : new HMACSHA256(key))
            {
                hma.ComputeHash(Encoding.UTF8.GetBytes(value));

                return hma.Hash;
            }
        }

        public string ByteToString(byte[] bytes)
        {
            return BitConverter.ToString(bytes)
                               .Replace("-", string.Empty)
                               .ToLower();
        }

        #region Singleton     

        private static Hashing _instance;

        private static readonly object Lock = new object();

        private Hashing() { }

        public static Hashing Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Hashing());
                }
            }
        }

        #endregion
    }
}