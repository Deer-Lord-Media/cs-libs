namespace Pokemon.Data
{
    public readonly struct Ability
    {
        public Ability(
            int id,
            int power,
            int accuracy,
            Type type,
            Stats.Name stat,
            System.TimeSpan coolDownDuration,
            Status status
        )
        {
            this.Id = id;
            this.Power = power;
            this.Accuracy = accuracy;
            this.Type = type;
            this.Stat = stat;
            this.CoolDownDuration = coolDownDuration;
            this.Status = status;
        }
        public readonly int Id;
        public readonly int Power;
        public readonly int Accuracy;
        public readonly Type Type;
        public readonly Stats.Name Stat;
        public readonly Status Status;
        public readonly System.TimeSpan CoolDownDuration;
    }
}
