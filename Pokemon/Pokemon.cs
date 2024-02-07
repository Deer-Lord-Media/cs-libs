using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using CSLibs.Items;

namespace Pokemon
{
    public class Monster
    {
        public Monster(
            Stats.Calculated stats,
            Data.Model data,
            Data.Ability[]? abilities = null,
            Data.Status status = Pokemon.Data.Status.None,
            int hitReduction = 0,
            int damageTaken = 0
        )
        {
            this.stats = stats;
            this.Data = data;
            this.abilities = new Data.Ability[4];
            if (abilities != null)
            {
                Array.Copy(abilities, this.abilities, abilities.Length);
            }
            this.Status = status;
            this.coolDown = new CoolDown();
            this.hitChance = new Random();
            this.hitReduction = hitReduction;
            this.damageTaken = damageTaken;
            this.resist = 100;
        }

        public readonly Data.Model Data;
        public Stats.Calculated Stats
        {
            get
            {
                return this.stats;
            }
        }
        private Stats.Calculated stats;

        public int CurrentHealth
        {
            get
            {
                int totalDamage = this.Stats.Current.Health - this.damageTaken;
                return (int)Math.Max(totalDamage, 0);
            }
        }
        private int damageTaken;

        public Data.Ability[] Abilities
        {
            get
            {
                return this.abilities;
            }
        }
        private Data.Ability[] abilities;
        public float CoolDownPercent
        {
            get
            {
                float retval = 0f;

                if (this.coolDown.Busy)
                {
                    TimeSpan spent = DateTime.UtcNow - this.coolDown.StartedAt;
                    retval = (float)(spent / this.coolDown.Duration);
                }
                return Math.Clamp(retval, 0.0f, 1.0f);
            }
        }
        private CoolDown coolDown;

        // battle status
        public Data.Status Status;
        public int Accuracy
        {
            get
            {
                return this.hitChance.Next(100 + this.hitReduction);
            }
        }
        private Random hitChance;
        private int hitReduction;
        public int Resist
        {
            get
            {
                return this.resist;
            }
        }
        private int resist;

        public Data.Ability? UseAbility(int index)
        {
            Data.Ability ability = this.Abilities[index];

            if (
                // status check
                !Pokemon.Data.Condition.Unusuable.Contains(ability.Status)
                || this.Resist <= this.hitChance.Next(100)
                // misses start cooldown
                && this.coolDown.Start(ability.CoolDownDuration)
                // accuracy check
                && this.Accuracy <= ability.Accuracy   
            )
            {
                return ability;
            }
            return null;
        }

        public int TakeDamage(int damage)
        {
            return 0;
        }

        public void Defeat(Monster target)
        {
            int level = this.Stats.Level;
            int health = 0;
            int attack = 0;
            int defense = 0;
            int speed = 0;
            int special = 0;
            int experience = this.Stats.Experience;
            // get experience from target
            
            if (this.Data.Experience.Needed(this.Stats.Level + 1) >= experience)
            {
                level += 1;
                health = this.Data.BaseStats.Health / 50 + this.Stats.EVs.Health / 100;
                attack = this.Data.BaseStats.Attack / 50 + this.Stats.EVs.Attack / 100;
                defense = this.Data.BaseStats.Defense / 50 + this.Stats.EVs.Defense / 100;
                speed = this.Data.BaseStats.Speed / 50 + this.Stats.EVs.Speed / 100;
                special = this.Data.BaseStats.Special / 50 + this.Stats.EVs.Special / 100;
            }

            Pokemon.Stats.Basic baseStats = new Pokemon.Stats.Basic(
                this.Stats.Base.Health + health,
                this.Stats.Base.Attack + attack,
                this.Stats.Base.Defense + defense,
                this.Stats.Base.Speed + speed,
                this.Stats.Base.Special + special
            );
            Pokemon.Stats.Basic evs = this.Stats.EVs + target.Data.EVs;

            this.stats = new Pokemon.Stats.Calculated(
                level,
                experience,
                baseStats,
                this.Stats.IVs,
                evs
            );
        }
    }
}
