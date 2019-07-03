using Apex.Instagram.API.Constants;

namespace Apex.Instagram.API.Model.Account.Device
{
    internal class UserAgent
    {
        private const string UserAgentFormat = "Instagram {0} Android ({1}/{2}; {3}; {4}; {5}; {6}; {7}; {8}; {9}; {10})";

        private readonly DeviceInfo _device;

        public UserAgent(DeviceInfo device) { _device = device; }

        public string BuildUserAgent()
        {
            var manufacturerWithBrand = _device.Manufacturer;
            if ( !string.IsNullOrWhiteSpace(_device.Brand) )
            {
                manufacturerWithBrand += $"/{_device.Brand}";
            }

            return string.Format(UserAgentFormat, Version.Instance.InstagramVersion, _device.AndroidVersion, _device.AndroidRelease, _device.Dpi, _device.Resolution, manufacturerWithBrand, _device.Model, _device.Device, _device.Cpu, Constants.Request.Instance.UserAgentLocale, Version.Instance.VersionCode);
        }
    }
}