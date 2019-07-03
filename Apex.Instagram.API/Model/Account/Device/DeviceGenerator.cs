using Apex.Instagram.API.Utils;

namespace Apex.Instagram.API.Model.Account.Device
{
    internal class DeviceGenerator
    {
        public DeviceInfo Get() { return Randomizer.Instance.Item(Constants.Device.Instance.List); }

        #region Singleton     

        private static DeviceGenerator _instance;

        private static readonly object Lock = new object();

        private DeviceGenerator() { }

        public static DeviceGenerator Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new DeviceGenerator());
                }
            }
        }

        #endregion
    }
}