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
                int baseValue = (int)(this.Base.Health + this.IVs.Health) * 2;
                int ev = (int)Math.Ceiling(Math.Sqrt(this.EVs.Health) / 4);
                int numerator = (baseValue + ev) * this.Level;
                int divisor = (int)Math.Ceiling((double)(numerator / 100));
                int retval = divisor + this.Level + 10;
                return retval;
            }
        }

        public int Attack
        {
            get
            {
                int baseValue = (int)(this.Base.Attack + this.IVs.Attack) * 2;
                int ev = (int)Math.Ceiling(Math.Sqrt(this.EVs.Attack) / 4);
                int numerator = (baseValue + ev) * this.Level;
                int divisor = (int)Math.Ceiling((double)(numerator / 100));
                int retval = divisor + this.Level;
                return retval;
            }
        }

        public int Defense
        {
            get
            {
                int baseValue = (int)(this.Base.Defense + this.IVs.Defense) * 2;
                int ev = (int)Math.Ceiling(Math.Sqrt(this.EVs.Defense) / 4);
                int numerator = (baseValue + ev) * this.Level;
                int divisor = (int)Math.Ceiling((double)(numerator / 100));
                int retval = divisor + this.Level;
                return retval;
            }
        }

        public int Speed
        {
            get
            {
                int baseValue = (int)(this.Base.Speed + this.IVs.Speed) * 2;
                int ev = (int)Math.Ceiling(Math.Sqrt(this.EVs.Speed) / 4);
                int numerator = (baseValue + ev) * this.Level;
                int divisor = (int)Math.Ceiling((double)(numerator / 100));
                int retval = divisor + this.Level;
                return retval;
            }
        }

        public int Special
        {
            get
            {
                int baseValue = (int)(this.Base.Special + this.IVs.Special) * 2;
                int ev = (int)Math.Ceiling(Math.Sqrt(this.EVs.Special) / 4);
                int numerator = (baseValue + ev) * this.Level;
                int divisor = (int)Math.Ceiling((double)(numerator / 100));
                int retval = divisor + this.Level;
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
