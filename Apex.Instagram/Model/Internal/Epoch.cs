using System;
using System.Globalization;

namespace Apex.Instagram.Model.Internal
{
    internal class Epoch
    {
        public Epoch() { Value = Current; }

        public Epoch(double initial) { Value = initial; }

        private static double Current => (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

        public double Value { get; private set; }

        public void Update(double value) { Value = value; }

        public void Update() { Value = Current; }

        public static implicit operator TimeSpan(Epoch epoch) { return TimeSpan.FromSeconds(epoch.Value); }

        public static Epoch operator -(Epoch current, Epoch other) { return new Epoch(current.Value - other.Value); }

        public override string ToString() { return Value.ToString(CultureInfo.InvariantCulture); }
    }
}