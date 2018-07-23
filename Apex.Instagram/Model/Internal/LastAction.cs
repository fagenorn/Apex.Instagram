using System;

namespace Apex.Instagram.Model.Internal
{
    internal class LastAction
    {
        private readonly Epoch _last;

        private readonly TimeSpan _limit;

        public LastAction(TimeSpan limit, Epoch initial = null)
        {
            _limit = limit;
            _last  = initial ?? new Epoch(0);
        }

        public bool Passed => new Epoch() - _last > _limit;

        public void Update() { _last.Update(); }
    }
}