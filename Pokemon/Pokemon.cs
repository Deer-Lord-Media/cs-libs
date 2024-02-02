using System;
using CSLibs.Items;

namespace Pokemon
{
    public struct Monster
    {
        public Monster(
            int level,
            int experience,
            Stats baseStats,
            Stats ivs,
            Stats evs,
            MonsterData data,
            Ability[]? abilities = null,
            Status status = Status.None,
            Stats? statModifiers = null
        )
        {
            this.Level = level;
            this.Experience = experience;
            this.baseStats = baseStats;
            this.IVs = ivs;
            this.EVs = evs;
            this.Type1 = data.Type1;
            this.Type2 = data.Type2;
            Ability[] a = new Ability[4];
            if (abilities != null)
            {
                Array.Copy(abilities, a, abilities.Length);
            }
            this.Abilities = a;
            this.status = status;
            this.coolDown = new CoolDown();
            this.hitChance = new Random();
            this.hitReduction = 0;
            this.damageTaken = 0;
            this.statModifiers = (statModifiers == null)
                ? new Stats(1, 1, 1, 1, 1)
                : (Stats)statModifiers
            ;
        }

        public int Level;
        public int Experience;

        public int CurrentHealth
        {
            get
            {
                int totalDamage = this.Stats.Health - this.damageTaken;
                return (int)Math.Max(totalDamage, 0);
            }
        }

        public Stats BaseStats
        {
            get
            {
                return this.baseStats;
            }
            set
            {
                this.baseStats = value;
            }
        }
        private Stats baseStats;

        public Stats IVs;
        public Stats EVs;
        private Stats statModifiers;
        public Stats Stats
        {
            get
            {
                double baseValue;
                double ev;
                double numerator;
                double divisor;

                double health;
                double attack;
                double defense;
                double speed;
                double special;

                baseValue = (this.BaseStats.Health + this.IVs.Health) * 2;
                ev = Math.Sqrt(this.EVs.Health) / 4;
                numerator = (baseValue + ev) * this.Level;
                divisor = numerator / 100;
                health = Math.Ceiling(divisor + this.Level + 10);

                baseValue = (this.BaseStats.Attack + this.IVs.Attack) * 2;
                ev = Math.Sqrt(this.EVs.Attack) / 4;
                numerator = (baseValue + ev) * this.Level;
                divisor = numerator / 100;
                attack = Math.Ceiling(divisor + this.Level);

                baseValue = (this.BaseStats.Defense + this.IVs.Defense) * 2;
                ev = Math.Sqrt(this.EVs.Defense) / 4;
                numerator = (baseValue + ev) * this.Level;
                divisor = numerator / 100;
                defense = Math.Ceiling(divisor + this.Level);

                baseValue = (this.BaseStats.Speed + this.IVs.Speed) * 2;
                ev = Math.Sqrt(this.EVs.Speed) / 4;
                numerator = (baseValue + ev) * this.Level;
                divisor = numerator / 100;
                speed = Math.Ceiling(divisor + this.Level);

                baseValue = (this.BaseStats.Special + this.IVs.Special) * 2;
                ev = Math.Sqrt(this.EVs.Special) / 4;
                numerator = (baseValue + ev) * this.Level;
                divisor = numerator / 100;
                special = Math.Ceiling(divisor + this.Level);

                Stats stats = new Stats(
                    (int)health,
                    (int)attack,
                    (int)defense,
                    (int)speed,
                    (int)special
                );
                return stats * this.statModifiers;
            }
        }
        
        public Type Type1;
        public Type? Type2;
        public Ability[] Abilities;

        // battle status
        private int damageTaken;
        private Status status;
        private Random hitChance;
        private int hitReduction;
        private CoolDown coolDown;

        // public bool UseItem(Item item)
        // {
        //     bool retval = item.Use()
        //         && this.coolDown.Start(item.CoolDown.Duration)
        //     ;
        //     if (!retval) item.CoolDown.Reset();
        //     return retval;
        // }

        public int UseAbility(int index, Monster target)
        {
            int damage = 0;
            Ability ability = this.Abilities[index];

            // usability/hit check
            if (
                this.coolDown.Start(ability.CoolDownDuration)
                && this.hitChance.Next(100 + hitReduction) <= ability.Accuracy
            )
            {
                damage = (ability.Power != 0)
                    ? this.damageCalculation(ability, target)
                    : 0
                ;
                if (ability.Status != Status.None)
                {
                    // calculate application
                }
            }
            return damage;
        }

        public void TakeDamage(int value)
        {
            int incoming = this.damageTaken + value;
            this.damageTaken = Math.Clamp(incoming, 0, this.Stats.Health);
            // status?
        }

        private int damageCalculation(Ability move, Monster target)
        {
            int attack;
            int defense;
            int damage;

            // find stat values used for attack and defense calculation
            var userStats = this.Stats;
            var targetStats = target.Stats;
            attack = move.Stat switch
            {
                StatName.Attack => userStats.Attack,
                StatName.Defense => userStats.Defense,
                StatName.Special => userStats.Special,
                StatName.Speed => userStats.Speed,
                StatName.SpcAttack => userStats.SpcAttack,
                StatName.SpcDefense => userStats.SpcDefense,
                _ => move.Power,
            };
            defense = move.Stat switch
            {
                StatName.Attack => targetStats.Defense,
                StatName.Defense => targetStats.Attack,
                StatName.Special => targetStats.Special,
                StatName.Speed => targetStats.Speed,
                StatName.SpcAttack => targetStats.SpcDefense,
                StatName.SpcDefense => targetStats.SpcAttack,
                _ => target.Level,
            };

            // base formula
            double value = 2 * this.Level / 5 + 2;
            value *= move.Power * (attack/defense);
            value /= 50;
            value = Math.Ceiling(value) + 2;
            
            // STAB: same-type attack bonus
            if (this.Type1 == move.Type || this.Type2 != null && this.Type2 == move.Type)
            {
                value = Math.Ceiling(value * 1.5);
            }
            
            // type matchups
            double modifier1 = Matchups.Table[move.Type][target.Type1];
            double modifier2 = 1;
            if (target.Type2 != null)
            {
                Type type2 = (Type)target.Type2;
                modifier2 = Matchups.Table[move.Type][type2];
            }
            value *= modifier1 * modifier2;

            damage = (int)Math.Ceiling(value);
            //damage = this.randomize(damage);
            return damage;
        }
    }
}
