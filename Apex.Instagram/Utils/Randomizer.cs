﻿using System;
using System.Collections.Generic;

namespace Apex.Instagram.Utils
{
    internal class Randomizer
    {
        private readonly Random _random;

        public int Number(int max, int min = 0)
        {
            max += 1;

            return _random.Next(min, max);
        }

        public T Item<T>(IList<T> list)
        {
            var amount = Number(list.Count - 1);

            return list[amount];
        }

        #region Singleton     

        private static Randomizer _instance;

        private static readonly object Lock = new object();

        private Randomizer() { _random = new Random(); }

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