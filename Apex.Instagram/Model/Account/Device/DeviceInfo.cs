using MessagePack;

namespace Apex.Instagram.Model.Account.Device
{
    [MessagePackObject]
    internal class DeviceInfo
    {
        public DeviceInfo() { _userAgentBuilder = new UserAgent(this); }

        #region Fields

        private readonly UserAgent _userAgentBuilder;

        private string _userAgent;

        #endregion

        #region Properties

        [Key(0)]
        public string AndroidRelease { get; set; }

        [Key(1)]
        public string AndroidVersion { get; set; }

        [Key(2)]
        public string Device { get; set; }

        [Key(3)]
        public string Resolution { get; set; }

        [Key(4)]
        public string Manufacturer { get; set; }

        [Key(5)]
        public string Model { get; set; }

        [Key(6)]
        public string Brand { get; set; }

        [Key(7)]
        public string Cpu { get; set; }

        [Key(8)]
        public string Dpi { get; set; }

        [IgnoreMember]
        public string UserAgent => _userAgent ?? (_userAgent = _userAgentBuilder.BuildUserAgent());

        #endregion
    }
}