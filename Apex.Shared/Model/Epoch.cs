using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Apex.Shared.Model
{
    [DataContract]
    public class Epoch
    {
        public Epoch(double initial = double.NaN) { Value = double.IsNaN(initial) ? Current : initial; }

        #region Properties

        [DataMember(Order = 0)]
        public double Value { get; private set; }

        #endregion

        public static double Current => (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

        public void Update(double value) { Value = value; }

        public void Update() { Value = Current; }

        public static implicit operator TimeSpan(Epoch epoch) { return TimeSpan.FromSeconds(epoch.Value); }

        public static implicit operator int(Epoch epoch) { return (int) epoch.Value; }

        public static Epoch operator -(Epoch current, Epoch other) { return new Epoch(current.Value - other.Value); }

        public override string ToString() { return Value.ToString(CultureInfo.InvariantCulture); }
    }
}