using System;

namespace Apex.Instagram.API.Constants
{
    internal class Delays
    {
        public TimeSpan AppRefreshInterval { get; } = TimeSpan.FromSeconds(1800);

        public TimeSpan ExperimentsRefreshInterval { get; } = TimeSpan.FromMinutes(120);

        public TimeSpan CookieSaveInterval { get; } = TimeSpan.FromMilliseconds(50); // Save new cookies only every 50 ms. Reducing this will cause saves to occur more often at the cost of performance.

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