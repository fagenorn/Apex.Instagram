namespace Apex.Instagram.Constants
{
    internal class Version
    {
        public string SigningKeyVersion { get; } = "4";

        public string InstagramVersion { get; } = "42.0.0.19.95";

        public string SigningKey { get; } = "673581b0ddb792bf47da5f9ca816b613d7996f342723aa06993a3f0552311c7d";

        #region Singleton     

        private static Version _instance;

        private static readonly object Lock = new object();

        private Version() { }

        public static Version Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Version());
                }
            }
        }

        #endregion
    }
}