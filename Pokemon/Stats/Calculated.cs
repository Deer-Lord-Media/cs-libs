using System;
using Pokemon.Data;

namespace Pokemon.Stats
{
    public readonly struct Calculated
    {
        public Calculated(
            int level,
            int experience,
            Basic baseStats,
            Basic ivs,
            Basic evs
        )
        {
            this.Level = level;
            this.Experience = experience;
            this.Base = baseStats;
            this.IVs = ivs;
            this.EVs = evs;
        }
        public readonly int Level;
        public readonly int Experience;

        public readonly Basic Base;
        public readonly Basic IVs;
        public readonly Basic EVs;

        private int calculateStat(int value, int iv, int ev, int offset)
        {
            double baseValue;
            double numerator;
            double divisor;   

            baseValue = (value + iv) * 2;
            numerator = (baseValue + Math.Sqrt(ev) / 4) * this.Level;
            divisor = numerator / 100;
            return (int)Math.Ceiling(divisor + offset);
        }

        public Basic Current
        {
            get
            {
                int health = calculateStat(this.Base.Health, this.IVs.Health, this.EVs.Health, this.Level + 10);
                int attack = calculateStat(this.Base. Attack, this.IVs. Attack, this.EVs. Attack, 5);
                int defense = calculateStat(this.Base.Defense, this.IVs.Defense, this.EVs.Defense, 5);
                int speed = calculateStat(this.Base.Speed, this.IVs.Speed, this.EVs.Speed, 5);
                int special = calculateStat(this.Base.Special, this.IVs.Special, this.EVs.Special, 5);
                Basic stats = new Basic(
                    health,
                    attack,
                    defense,
                    speed,
                    special
                );
                return stats;
            }
        }
    }
}