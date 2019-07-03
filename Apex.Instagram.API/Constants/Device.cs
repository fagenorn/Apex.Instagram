using Apex.Instagram.API.Model.Account.Device;

namespace Apex.Instagram.API.Constants
{
    internal class Device
    {
        public DeviceInfo[] List { get; } =
            {
                new DeviceInfo
                {
                    AndroidVersion = "24",
                    AndroidRelease = "7.0",
                    Dpi            = "380dpi",
                    Resolution     = "1080x1920",
                    Manufacturer   = "OnePlus",
                    Model          = "ONEPLUS A5010",
                    Device         = "OnePlus3T",
                    Cpu            = "qcom"
                },

                new DeviceInfo
                {
                    AndroidVersion = "23",
                    AndroidRelease = "6.0.1",
                    Dpi            = "640dpi",
                    Resolution     = "1440x2392",
                    Manufacturer   = "LGE",
                    Brand          = "lge",
                    Model          = "RS988",
                    Device         = "h1",
                    Cpu            = "h1"
                },

                new DeviceInfo
                {
                    AndroidVersion = "24",
                    AndroidRelease = "7.0",
                    Dpi            = "640dpi",
                    Resolution     = "1440x2560",
                    Manufacturer   = "HUAWEI",
                    Model          = "LON-L29",
                    Device         = "HWLON",
                    Cpu            = "hi3660"
                },

                new DeviceInfo
                {
                    AndroidVersion = "23",
                    AndroidRelease = "6.0.1",
                    Dpi            = "640dpi",
                    Resolution     = "1440x2560",
                    Manufacturer   = "ZTE",
                    Model          = "ZTE A2017U",
                    Device         = "ailsa_ii",
                    Cpu            = "qcom"
                },

                new DeviceInfo
                {
                    AndroidVersion = "23",
                    AndroidRelease = "6.0.1",
                    Dpi            = "640dpi",
                    Resolution     = "1440x2560",
                    Manufacturer   = "samsung",
                    Model          = "SM-G935F",
                    Device         = "hero2lte",
                    Cpu            = "samsungexynos8890"
                },

                new DeviceInfo
                {
                    AndroidVersion = "23",
                    AndroidRelease = "6.0.1",
                    Dpi            = "640dpi",
                    Resolution     = "1440x2560",
                    Manufacturer   = "samsung",
                    Model          = "SM-G930F",
                    Device         = "herolte",
                    Cpu            = "samsungexynos8890"
                }
            };

        #region Singleton     

        private static Device _instance;

        private static readonly object Lock = new object();

        private Device() { }

        public static Device Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Device());
                }
            }
        }

        #endregion
    }
}