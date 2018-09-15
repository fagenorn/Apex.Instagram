﻿using System.Collections.Generic;
using System.Text;

using Apex.Instagram.Constants;
using Apex.Instagram.Request.Model;
using Apex.Instagram.Utils;

namespace Apex.Instagram.Request.Signature
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
            var data   = new StringBuilder();
            data.Append("{");

            foreach ( var param in postParams )
            {
                if ( param.Value.Sign )
                {
                    data.Append($"\"{param.Key}\":");
                    data.Append(param.Value.Serialize());
                    data.Append(",");
                }
                else
                {
                    result[param.Key] = param.Value;
                }
            }

            if ( data[data.Length - 1] == ',' )
            {
                data.Length--;
            }

            data.Append("}");

            var signedString = $"{GenerateSignature(data.ToString())}.{data}";
            result["ig_sig_key_version"] = new Parameter(new PostString(Version.Instance.SigningKeyVersion), false);
            result["signed_body"]        = new Parameter(new PostString(signedString), false);

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