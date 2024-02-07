using System;
using System.Linq;

namespace Pokemon
{
    public class Battle
    {
        public int UseAbility(int index, Monster user, Monster target)
        {
            int damage = 0; 
            Data.Ability? ability = user.UseAbility(index);

            if (ability != null)
            {
                Data.Ability a = (Data.Ability)ability;
                damage = (a.Power != 0)
                    ? this.damageCalculation(a, user, target)
                    : 0
                ;
                target.TakeDamage(damage);
                if (a.Status != Data.Status.None && target.Accuracy < 50)
                {
                    target.Status = a.Status;
                }
            }
            return damage;
        }

        private int damageCalculation(Data.Ability move, Monster user, Monster target)
        {
            int attack;
            int defense;
            int damage;

            // find stat values used for attack and defense calculation
            var userStats = user.Stats.Current;
            var targetStats = target.Stats.Current;
            attack = move.Stat switch
            {
                Stats.Name.Attack => userStats.Attack,
                Stats.Name.Defense => userStats.Defense,
                Stats.Name.Special => userStats.Special,
                Stats.Name.Speed => userStats.Speed,
                Stats.Name.SpcAttack => userStats.SpcAttack,
                Stats.Name.SpcDefense => userStats.SpcDefense,
                _ => move.Power,
            };
            defense = move.Stat switch
            {
                Stats.Name.Attack => targetStats.Defense,
                Stats.Name.Defense => targetStats.Attack,
                Stats.Name.Special => targetStats.Special,
                Stats.Name.Speed => targetStats.Speed,
                Stats.Name.SpcAttack => targetStats.SpcDefense,
                Stats.Name.SpcDefense => targetStats.SpcAttack,
                _ => target.Stats.Level,
            };

            // base formula
            double value = 2 * user.Stats.Level / 5 + 2;
            value *= move.Power * (attack/defense) / 50;
            value = Math.Ceiling(value) + 2;
            
            // STAB: same-type attack bonus
            if (user.Data.Type1 == move.Type || user.Data.Type2 != null && user.Data.Type2 == move.Type)
            {
                value = Math.Ceiling(value * 1.5);
            }
            
            // type matchups
            double modifier1 = Data.Matchups.Factor(move.Type, target.Data.Type1);
            double modifier2 = 1;
            if (target.Data.Type2 != null)
            {
                Data.Type type2 = (Data.Type)target.Data.Type2;
                modifier2 = Data.Matchups.Factor(move.Type, type2);
            }
            value *= modifier1 * modifier2;

            damage = (int)Math.Ceiling(value);
            //damage = this.randomize(damage);
            return damage;
        }

        public Stats.Basic StatReward(Monster target)
        {
            Stats.Basic statReward = new Stats.Basic(
                target.Stats.Base.Health / target.Stats.Level,
                target.Stats.Base.Attack / target.Stats.Level,
                target.Stats.Base.Defense / target.Stats.Level,
                target.Stats.Base.Speed / target.Stats.Level,
                target.Stats.Base.Special / target.Stats.Level
            );
            return statReward;
        }

        public Stats.Basic EVReward(Monster target)
        {
            Stats.Basic evReward = new Stats.Basic(
                target.Stats.EVs.Health / target.Stats.Level,
                target.Stats.EVs.Attack / target.Stats.Level,
                target.Stats.EVs.Defense / target.Stats.Level,
                target.Stats.EVs.Speed / target.Stats.Level,
                target.Stats.EVs.Special / target.Stats.Level
            );
            return evReward;
        }

        public int EXPReward(Monster target)
        {
            int retval = 0;

            return retval;
        }
    }
}