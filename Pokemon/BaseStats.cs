namespace monsters
{
    public class BaseStats
    {
        public BaseStats(
            float health,
            float attack,
            float defense,
            float special,
            float speed
        )
        {
            this.Health = health;
            this.Attack = attack;
            this.Defense = defense;
            this.Special = special;
            this.Speed = speed;
        }
        public readonly float Health;
        public readonly float Attack;
        public readonly float Defense;
        public readonly float Special;
        public readonly float Speed;

        public static BaseStats operator+ (BaseStats b, BaseStats c)
        {
            BaseStats a = new BaseStats(
                b.Health + c.Health,
                b.Attack + c.Attack,
                b.Defense + c.Defense,
                b.Special + c.Special,
                b.Speed + c.Speed
            );
            return a;
        }

        public static BaseStats operator- (BaseStats b, BaseStats c)
        {
            BaseStats a = new BaseStats(
                b.Health - c.Health,
                b.Attack - c.Attack,
                b.Defense - c.Defense,
                b.Special - c.Special,
                b.Speed - c.Speed
            );
            return a;
        }
    }
}
