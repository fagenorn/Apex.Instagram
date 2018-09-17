using System;

namespace Apex.Instagram.Constants
{
    internal class Delays
    {
        public TimeSpan AppRefreshInterval { get; } = TimeSpan.FromSeconds(1800);

        #region Singleton     

        private static Delays _instance;

        private static readonly object Lock = new object();

        private Delays() { }

        public static Delays Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Delays());
                }
            }
        }

        #endregion
    }
}