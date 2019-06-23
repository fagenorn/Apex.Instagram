using System;

namespace Apex.Instagram.Utils
{
    internal class Time
    {
        public int GetTimezoneOffset()
        {
            return (int) DateTimeOffset.Now.Offset.TotalSeconds;
        }

        #region Singleton     

        private static Time _instance;

        private static readonly object Lock = new object();

        private Time() { }

        public static Time Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Time());
                }
            }
        }

        #endregion
    }
}