using System;
using System.Collections.Generic;
using System.Threading;

namespace Apex.Instagram.Utils
{
    internal class Randomizer
    {
        private static readonly Random GlobalRandom = new Random();

        private readonly ThreadLocal<Random> _random;

        private static Random NewRandom()
        {
            lock (Lock)
            {
                return new Random(GlobalRandom.Next());
            }
        }

        public int Number(int exclusiveMax, int inclusiveMin = 0) { return _random.Value.Next(inclusiveMin, exclusiveMax); }

        public T Item<T>(IList<T> list)
        {
            var amount = Number(list.Count);

            return list[amount];
        }

        #region Singleton     

        private static Randomizer _instance;

        private static readonly object Lock = new object();

        private Randomizer() { _random = new ThreadLocal<Random>(NewRandom); }

        public static Randomizer Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new Randomizer());
                }
            }
        }

        #endregion
    }
}