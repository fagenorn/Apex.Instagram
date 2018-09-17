using Apex.Instagram.Model.Account.Device;

using MessagePack;

namespace Apex.Instagram.Model.Account
{
    [MessagePackObject]
    internal class AccountInfo
    {
        [Key(0)]
        public string Username { get; internal set; }

        [Key(1)]
        public string Password { get; internal set; }

        [Key(2)]
        public string Uuid { get; internal set; }

        [Key(3)]
        public string AdvertisingId { get; internal set; }

        [Key(4)]
        public string SessionId { get; internal set; }

        [Key(5)]
        public string PhoneId { get; internal set; }

        [Key(6)]
        public string DeviceId { get; internal set; }

        [Key(7)]
        public DeviceInfo DeviceInfo { get; internal set; }

        [Key(8)]
        public string AccountId { get; internal set; }
    }
}