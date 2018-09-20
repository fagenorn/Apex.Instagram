using System.Runtime.Serialization;

using Apex.Instagram.Model.Internal;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Token
    {
        private const int DefaultTtl = 3600;

        public int ExpiresAt()
        {
            var ttl = Ttl;
            if ( ttl == 0 )
            {
                ttl = DefaultTtl;
            }

            return new Epoch() + ttl ?? 0;
        }

        #region Properties

        [DataMember(Name = "carrier_name")]
        public string CarrierName { get; set; }

        [DataMember(Name = "carrier_id")]
        public int? CarrierId { get; set; }

        [DataMember(Name = "ttl")]
        public int? Ttl { get; set; }

        [DataMember(Name = "features")]
        public dynamic Features { get; set; }

        [DataMember(Name = "request_time")]
        public ulong? RequestTime { get; set; }

        [DataMember(Name = "token_hash")]
        public string TokenHash { get; set; }

        [DataMember(Name = "rewrite_rules")]
        public RewriteRule[] RewriteRules { get; set; }

        [DataMember(Name = "enabled_wallet_defs_keys")]
        public dynamic EnabledWalletDefsKeys { get; set; }

        [DataMember(Name = "deadline")]
        public string Deadline { get; set; }

        [DataMember(Name = "zero_cms_fetch_interval_seconds")]
        public int? ZeroCmsFetchIntervalSeconds { get; set; }

        #endregion
    }
}