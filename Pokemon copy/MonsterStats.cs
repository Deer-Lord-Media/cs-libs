using System;

namespace monsters
{
    public struct MonsterStats
    {

        public MonsterStats(
            int level,
            BaseStats baseStats,
            BaseStats ivs,
            BaseStats evs
        )
        {
            this.level = level;
            this.Base = baseStats;
            this.IVs = ivs;
            this.EVs = evs;
        }
        public int Level {
            readonly get
            {
                return this.level;
            }
            set
            {
                this.level = Math.Clamp(value, 0, Constants.MAX_LEVEL);
            }    
        }
        private int level;
        public BaseStats Base;
        public BaseStats IVs;
        public BaseStats EVs;

        public int Health
        {
            get
            {
                double baseValue = (this.Base.Health + this.IVs.Health) * 2;
                double ev = Math.Sqrt(this.EVs.Health) / 4;
                double numerator = (baseValue + ev) * this.Level;
                double divisor = numerator / 100;
                int retval = (int)Math.Ceiling(divisor + this.Level + 10);
                return retval;
            }
        }

        public int Attack
        {
            get
            {
                double baseValue = (this.Base.Attack + this.IVs.Attack) * 2;
                double ev = Math.Sqrt(this.EVs.Attack) / 4;
                double numerator = (baseValue + ev) * this.Level;
                double divisor = numerator / 100;
                int retval = (int)Math.Ceiling(divisor + this.Level);
                return retval;
            }
        }

        public int Defense
        {
            get
            {
                double baseValue = (this.Base.Defense + this.IVs.Defense) * 2;
                double ev = Math.Sqrt(this.EVs.Defense) / 4;
                double numerator = (baseValue + ev) * this.Level;
                double divisor = numerator / 100;
                int retval = (int)Math.Ceiling(divisor + this.Level);
                return retval;
            }
        }

        public int Speed
        {
            get
            {
                double baseValue = (this.Base.Speed + this.IVs.Speed) * 2;
                double ev = Math.Sqrt(this.EVs.Speed) / 4;
                double numerator = (baseValue + ev) * this.Level;
                double divisor = numerator / 100;
                int retval = (int)Math.Ceiling(divisor + this.Level);
                return retval;
            }
        }

        public int Special
        {
            get
            {
                double baseValue = (this.Base.Special + this.IVs.Special) * 2;
                double ev = Math.Sqrt(this.EVs.Special) / 4;
                double numerator = (baseValue + ev) * this.Level;
                double divisor = numerator / 100;
                int retval = (int)Math.Ceiling(divisor + this.Level);
                return retval;
            }
        }
        public int SpecialAttack
        {
            get
            {
                return (int)Math.Ceiling((double)(this.Attack + this.Special) / 2);
            }
        }

        public int SpecialDefense
        {
            get
            {
                return (int)Math.Ceiling((double)(this.Defense + this.Special) / 2);
            }
        }
    }
}
