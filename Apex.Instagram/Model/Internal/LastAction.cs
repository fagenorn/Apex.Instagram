using System;

using MessagePack;

namespace Apex.Instagram.Model.Internal
{
    [MessagePackObject]
    internal class LastAction
    {
        public LastAction(TimeSpan limit, Epoch initial = null)
        {
            Limit = limit;
            Last  = initial ?? new Epoch(0);
        }

        public void Update() { Last.Update(); }

        #region Properties

        [Key(0)]
        public TimeSpan Limit { get; internal set; }

        [Key(1)]
        public Epoch Last { get; }

        [IgnoreMember]
        public bool Passed => new Epoch() - Last > Limit;

        #endregion
    }
}