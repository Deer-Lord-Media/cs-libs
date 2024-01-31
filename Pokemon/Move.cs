using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace monsters
{
    public struct Move
    {
        public Move(
            int power,
            int accuracy,
            Element type,
            StatName stat
        )
        {
            this.Power = power;
            this.Accuracy = accuracy;
            this.Type = type;
            this.Stat = stat;
            this.randomizer = new Random();
            this.hitCheck = new Random();
        }

        public int Power;
        public int Accuracy;
        public Element Type;
        public StatName Stat;
        private Random hitCheck;
        private Random randomizer;
        private int randomize(int value)
        {
            int retval = value;
            
            if (value > 1)
            {
                double _ = this.randomizer.Next(217, 256);
                _ /= 255;
                _ *= value;
                retval = (int)Math.Ceiling(_);
            }
            return retval;
        }
        
        public int Damage(Pokemon user, Pokemon target)
        {
            int attack;
            int defense;
            int retval = 0;

            if (this.Power != 0)
            {
                var userStats = user.CurrentStats;
                var targetStats = target.CurrentStats;
                attack = this.Stat switch
                {
                    StatName.Attack => (int)userStats.Attack,
                    StatName.Defense => (int)userStats.Defense,
                    StatName.Special => (int)userStats.Special,
                    StatName.Speed => (int)userStats.Speed,
                    StatName.SpecialAttack => (int)userStats.SpecialAttack,
                    StatName.SpecialDefense => (int)userStats.SpecialDefense,
                    _ => this.Power,
                };
                defense = this.Stat switch
                {
                    StatName.Attack => (int)targetStats.Defense,
                    StatName.Defense => (int)targetStats.Attack,
                    StatName.Special => (int)targetStats.Special,
                    StatName.Speed => (int)targetStats.Speed,
                    StatName.SpecialAttack => (int)targetStats.SpecialDefense,
                    StatName.SpecialDefense => (int)targetStats.SpecialAttack,
                    _ => target.PermanentStats.Level,
                };

                // base formula
                double _ = (2 * user.PermanentStats.Level) / 5 + 2;
                int level = (int)Math.Ceiling(_);
                _ = this.Power * (attack/defense);
                int power = (int)Math.Ceiling(_);
                _ = (level * power) / 50;
                int value = (int)Math.Ceiling(_) + 2;
                
                // STAB: same-type attack bonus
                if (user.Type1 == this.Type || user.Type2 != null && user.Type2 == this.Type)
                {
                    value = (int)Math.Ceiling(value * 1.5);
                }
                
                // type matchups
                double modifier1 = Matchups.Table[this.Type][target.Type1];
                double modifier2 = 1;
                if (target.Type2 != null)
                {
                    Element type2 = (Element)target.Type2;
                    modifier2 = Matchups.Table[this.Type][type2];
                }
                double modifier = modifier1 * modifier2;

                retval = (int)Math.Ceiling(value * modifier);
                // retval = this.randomize(retval);
            }
            return retval;
        }

        public bool Hit(Pokemon user, Pokemon target)
        {
            int hitCheck = this.hitCheck.Next(100) + user.HitReduction;
            // how to use speed for accuracy/avoidance?
            bool retval = hitCheck <= this.Accuracy;

            return retval;
        }
    }
}