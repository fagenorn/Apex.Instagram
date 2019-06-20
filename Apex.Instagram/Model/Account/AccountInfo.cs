using Apex.Instagram.Model.Account.Device;

using MessagePack;

namespace Apex.Instagram.Model.Account
{
    [MessagePackObject]
    internal class AccountInfo
    {
        [Key(0)]
        public string Username { get; set; }

        [Key(1)]
        public string Password { get; set; }

        [Key(2)]
        public string Uuid { get; set; }

        [Key(3)]
        public string AdvertisingId { get; set; }

        [Key(4)]
        public string SessionId { get; set; }

        [Key(5)]
        public string PhoneId { get; set; }

        [Key(6)]
        public string DeviceId { get; set; }

        [Key(7)]
        public DeviceInfo DeviceInfo { get; set; }

        [Key(8)]
        public ulong AccountId { get; set; }
    }
}