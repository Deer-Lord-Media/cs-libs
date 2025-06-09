using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using CoolDown;

namespace Buffs
{
    public class Buff
    {
        public Buff(Data data, CoolDown.Tracker coolDown)
        {
            this.data = data;
            this.coolDown = coolDown;
        }
        public readonly Data data;
        public readonly CoolDown.Tracker coolDown;

        public override int GetHashCode()
        {
            return (this.data.Id * 397) ^ (this.coolDown.IsActive ? 1 : 0);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Buff))
            {
                return false;
            }

            return (Buff)obj == this;
        }

        /// <summary>
        /// Check if b1 takes precedence over b2.
        /// </summary>
        /// <param name="b1">The first buff to compare.</param>
        /// <param name="b2">The second buff to compare.</param>
        /// <returns>If precedence is met or not.</returns>
        public static bool operator >(Buff? b1, Buff? b2)
        {
            if (b1 is null) return false;
            if (b2 is null) return true;
            return b1.data.PrecedenceOver.Contains(b2.data);
        }

        /// <summary>
        /// Check if b2 takes precedence over b1.
        /// </summary>
        /// <param name="b1">The first buff to compare.</param>
        /// <param name="b2">The second buff to compare.</param>
        /// <returns>If precedence is met or not.</returns>
        public static bool operator <(Buff? b1, Buff? b2)
        {
            if (b2 is null) return false;
            if (b1 is null) return true;
            return b2.data.PrecedenceOver.Contains(b1.data);
        }

        /// <summary>
        /// Compares two buffs to see if they are mutually exclusive with each other.
        /// </summary>
        /// <param name="b1">The first buff to compare.</param>
        /// <param name="b2">The second buff to compare.</param>
        /// <returns>If buffs are mutually exclusive.</returns>
        public static bool operator ==(Buff? b1, Buff? b2)
        {
            if (Equals(b1, b2)) return true;
            if (b1 is null || b2 is null) return false;
            return b1.data.MutuallyExclusive.Contains(b2.data) || b2.data.MutuallyExclusive.Contains(b1.data);
        }

        /// <summary>
        /// Compares two buffs to see if they are unrelated to each other.
        /// </summary>
        /// <param name="b1">The first buff to compare.</param>
        /// <param name="b2">The second buff to compare.</param>
        /// <returns>If the buffs are not related.</returns>
        public static bool operator !=(Buff? b1, Buff? b2)
        {
            if (b1 is null && b2 is null) return false;
            if (b1 is null || b2 is null) return true;
            return (
                b1.data.Id != b2.data.Id  // buffs are not the same
                && !b1.data.MutuallyExclusive.Contains(b2.data) // buffs not in same category
                && !b2.data.MutuallyExclusive.Contains(b1.data)
                && !b1.data.PrecedenceOver.Contains(b2.data) // buffs don't have some kind of precedence link
                && !b2.data.PrecedenceOver.Contains(b1.data)
            );
        }
    }
}
