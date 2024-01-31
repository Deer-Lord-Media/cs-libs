using System;

namespace monsters
{
    public enum MoveType
    {
        Normal,
        Fire,
        Water,
        Electric,
        Grass,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon
    }

    public enum StatType
    {
        Health,
        Attack,
        Defense,
        Special,
        Speed,
        SpecialAttack,
        SpecialDefense
    }

    public struct Move
    {
        public Move(
            int power,
            int accuracy,
            MoveType type,
            StatType stat
        )
        {
            this.Power = power;
            this.Accuracy = accuracy;
            this.Type = type;
            this.Stat = stat;
        }

        public int Power;
        public int Accuracy;
        public MoveType Type;
        public StatType Stat;
        
        public int Damage(BattleStats userStats, BattleStats targetStats)
        {
            int modifier;
            int blocker;
            int retval = 0;

            if (this.Power != 0)
            { 
                modifier = this.Stat switch
                {
                    StatType.Attack => (int)userStats.Attack,
                    StatType.Defense => (int)userStats.Defense,
                    StatType.Special => (int)userStats.Special,
                    StatType.Speed => (int)userStats.Speed,
                    StatType.SpecialAttack => (int)userStats.SpecialAttack,
                    StatType.SpecialDefense => (int)userStats.SpecialDefense,
                    _ => this.Power,
                };
                blocker = this.Stat switch
                {
                    StatType.Attack => (int)targetStats.Attack,
                    StatType.Defense => (int)targetStats.Defense,
                    StatType.Special => (int)targetStats.Special,
                    StatType.Speed => (int)targetStats.Speed,
                    StatType.SpecialAttack => (int)targetStats.SpecialAttack,
                    StatType.SpecialDefense => (int)targetStats.SpecialDefense,
                    _ => 0,
                };
                retval = Math.Clamp(
                    ((modifier + this.Power) / 2) - blocker,
                    0,
                    targetStats.Health
                );
            }
            return retval;
        }
    }
}