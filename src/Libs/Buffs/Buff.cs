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

        public static bool operator >(Buff? b1, Buff? b2)
        {
            if (b1 is null && b2 is null) return true;
            if (b1 is null || b2 is null) return false;
            return b1.data.PrecedenceOver.Contains(b2.data);
        }

        public static bool operator <(Buff? b1, Buff? b2)
        {
            if (b1 is null && b2 is null) return true;
            if (b1 is null || b2 is null) return false;
            return b2.data.PrecedenceOver.Contains(b1.data);
        }

        public static bool operator ==(Buff? b1, Buff? b2)
        {
            if (b1 is null && b2 is null) return true;
            if (b1 is null || b2 is null) return false;
            return b1.data.EqualTo.Contains(b2.data) || b2.data.EqualTo.Contains(b1.data);
        }

        public static bool operator !=(Buff? b1, Buff? b2)
        {
            if (b1 is null && b2 is null) return true;
            if (b1 is null || b2 is null) return false;
            // buffs are not the same buff
            // buffs are not in precedence categories
            return (
                b1.data.Id != b2.data.Id  // buffs are not the same
                && !b1.data.EqualTo.Contains(b2.data) // buffs not in same category
                && !b2.data.EqualTo.Contains(b1.data)
                && !b1.data.PrecedenceOver.Contains(b2.data) // buffs don't have some kind of precedence link
                && !b2.data.PrecedenceOver.Contains(b1.data)
            );
        }
    }
}
