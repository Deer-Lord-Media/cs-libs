using System;
using System.Reflection.Emit;

namespace Pokemon.Stats
{
    public class Stat
    {
        public Stat(Name name, Resource.Tracker root, Resource.Tracker iv, Resource.Tracker ev, Resource.Tracker buff)
        {
            this.name = name;
            this.root = root;
            this.iv = iv;
            this.ev = ev;
            this.buff = buff;
        }
        public readonly Name name;
        public readonly Resource.Tracker root;
        public readonly Resource.Tracker iv;
        public readonly Resource.Tracker ev;
        public readonly Resource.Tracker buff;

        public int Value(int level, Nature? nature)
        {
            double value = (
                ((2 * this.root.Value) + this.iv.Value + (this.ev.Value / 4))
                / 100
            );

            value += 5;

            if (this.name == Name.HitPoints)
            {
                value += (level/4) + 5;
                return (int)Math.Ceiling(value);
            }

            if (nature != null)
            {
                var change = 1.0f;
                if (this.name == nature.boost) change = 1.1f;
                if (this.name == nature.degrade) change = 0.9f;
                value *= change;
            }
            double factor = this.buff.Value >= 0
                ? 1.5f
                : 0.5f;
            factor = Math.Pow(factor, Math.Abs(this.buff.Value));
            return (int)Math.Ceiling(value * factor);
        }
    }
}
