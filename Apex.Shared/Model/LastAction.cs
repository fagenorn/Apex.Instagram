using System;
using System.Runtime.Serialization;

namespace Apex.Shared.Model
{
    [DataContract]
    public class LastAction
    {
        public LastAction(TimeSpan limit, Epoch initial = null)
        {
            Limit = limit;
            Last  = initial ?? new Epoch(0);
        }

        public void Update() { Last.Update(); }

        #region Properties

        [DataMember(Order = 0)]
        public TimeSpan Limit { get; set; }

        [DataMember(Order = 1)]
        public Epoch Last { get; }

        [IgnoreDataMember]
        public bool Passed => new Epoch() - Last > Limit;

        #endregion
    }
}