using System;

namespace Pokemon
{
    public struct Ability
    {

        public Ability(
            int power,
            int accuracy,
            Type type,
            StatName stat
        )
        {
            this.Power = power;
            this.Accuracy = accuracy;
            this.Type = type;
            this.Stat = stat;
            this.CoolDownDuration = new TimeSpan(0, 0, 0, 0, 0);
            this.Status = Pokemon.Status.None;

        }
        public int Power;
        public int Accuracy;
        public Type Type;
        public StatName Stat;
        public Status Status;
        public TimeSpan CoolDownDuration;
    }
}
