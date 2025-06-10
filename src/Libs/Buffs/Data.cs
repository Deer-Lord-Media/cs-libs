using System.Collections.Generic;
using System.Data.Common;

namespace Buffs
{
    public class Data
    {
        public Data(int id, string name, HashSet<Data> precedenceOver, HashSet<Data> mutuallyExclusive)
        {
            this.id = id;
            this.name = name;
            this.precedenceOver = precedenceOver;
            this.mutuallyExclusive = mutuallyExclusive;
        }
        public readonly int id;
        public readonly string name;
        private readonly HashSet<Data> precedenceOver;  // this buff will remove any buffs in this set.
        private readonly HashSet<Data> mutuallyExclusive; // this buff cannot coexist with any buffs in this set.

        public override int GetHashCode()
        {
            return this.id * 397;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Data))
            {
                return false;
            }

            return ((Data)obj).id == this.id;
        }

        /// <summary>
        /// Check if d1 takes precedence over d2.
        /// </summary>
        /// <param name="d1">The first buff to compare.</param>
        /// <param name="d2">The second buff to compare.</param>
        /// <returns>If precedence is met or not.</returns>
        public static bool operator >(Data? d1, Data? d2)
        {
            if (d1 is null) return false;
            if (d2 is null) return true;
            return d1.precedenceOver.Contains(d2);
        }

        /// <summary>
        /// Check if d2 takes precedence over d1.
        /// </summary>
        /// <param name="d1">The first buff to compare.</param>
        /// <param name="d2">The second buff to compare.</param>
        /// <returns>If precedence is met or not.</returns>
        public static bool operator <(Data? d1, Data? d2)
        {
            if (d2 is null) return false;
            if (d1 is null) return true;
            return d2.precedenceOver.Contains(d1);
        }

        /// <summary>
        /// Compares two buffs to see if they are mutually exclusive with each other.
        /// </summary>
        /// <param name="d1">The first buff to compare.</param>
        /// <param name="d2">The second buff to compare.</param>
        /// <returns>If buffs are mutually exclusive.</returns>
        public static bool operator ==(Data? d1, Data? d2)
        {
            if (Equals(d1, d2)) return true;
            if (d1 is null || d2 is null) return false;
            return d1.mutuallyExclusive.Contains(d2) || d2.mutuallyExclusive.Contains(d1);
        }

        /// <summary>
        /// Compares two buffs to see if they are unrelated to each other.
        /// </summary>
        /// <param name="d1">The first buff to compare.</param>
        /// <param name="d2">The second buff to compare.</param>
        /// <returns>If the buffs are not related.</returns>
        public static bool operator !=(Data? d1, Data? d2)
        {
            if (d1 is null && d2 is null) return false;
            if (d1 is null || d2 is null) return true;
            return (
                d1.id != d2.id  // buffs are not the same
                && !d1.mutuallyExclusive.Contains(d2) // buffs not in same category
                && !d2.mutuallyExclusive.Contains(d1)
                && !d1.precedenceOver.Contains(d2) // buffs don't have some kind of precedence link
                && !d2.precedenceOver.Contains(d1)
            );
        }
    }
}
