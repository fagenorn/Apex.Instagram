namespace Apex.Instagram.Constants
{
    internal class Facebook
    {
        public string FacebookOtaFields { get; } = "pdate%7Bdownload_uri%2Cdownload_uri_delta_base%2Cversion_code_delta_base%2Cdownload_uri_delta%2Cfallback_to_full_update%2Cfile_size_delta%2Cversion_code%2Cpublished_date%2Cfile_size%2Cota_bundle_type%2Cresources_checksum%7D";

        public string FacebookOrcaApplicationId { get; } = "124024574287414";

        #region Singleton     

        private static Facebook _instance;

        private static readonly object Lock = new object();

        private Facebook() { }

        public static Facebook Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Facebook());
                }
            }
        }

        #endregion
    }
}