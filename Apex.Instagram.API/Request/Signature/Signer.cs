using System.Collections.Generic;
using System.Text;

using Apex.Instagram.API.Constants;
using Apex.Instagram.API.Request.Model;
using Apex.Instagram.API.Utils;

using Utf8Json;

namespace Apex.Instagram.API.Request.Signature
{
    internal class Signer
    {
        public static string GenerateSignature(string value)
        {
            var key = Encoding.UTF8.GetBytes(Version.Instance.SigningKey);

            return Hashing.Instance.ByteToString(Hashing.Instance.Sha256(value, key));
        }

        public Dictionary<string, Parameter> Sign(Dictionary<string, Parameter> postParams)
        {
            var result = new Dictionary<string, Parameter>();
            var toSign = new Dictionary<string, string>();

            foreach ( var param in postParams )
            {
                if ( param.Value.Sign )
                {
                    toSign[param.Key] = param.Value.ToString();
                }
                else
                {
                    result[param.Key] = param.Value;
                }
            }

            var jsonString   = JsonSerializer.ToJsonString(toSign);
            var signedString = $"{GenerateSignature(jsonString)}.{jsonString}";
            result["ig_sig_key_version"] = new Parameter(Version.Instance.SigningKeyVersion, false);
            result["signed_body"]        = new Parameter(signedString, false);

            return result;
        }

        #region Singelton     

        private static Signer _instance;

        private static readonly object Lock = new object();

        private Signer() { }

        public static Signer Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Signer());
                }
            }
        }

        #endregion
    }
}