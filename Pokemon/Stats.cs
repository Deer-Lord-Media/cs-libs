namespace Pokemon
{
    public enum StatName
    {
        Health,
        Attack,
        Defense,
        Speed,
        Special,
        SpcAttack,
        SpcDefense
    }

    public struct Stats
    {
        public Stats(
            int health,
            int attack,
            int defense,
            int speed,
            int special
        )
        {
            Health = health;
            Attack = attack;
            Defense = defense;
            Speed = speed;
            Special = special;
        }
        public int Health;
        public int Attack;
        public int Defense;
        public int Speed;
        public int Special;

        public int SpcAttack
        {
            get
            {
                return (int)((this.Attack + this.Special) / 2);
            }
        }

        public int SpcDefense
        {
            get
            {
                return (int)((this.Defense + this.Special) / 2);
            }
        }

        public static Stats operator+ (Stats b, Stats c)
        {
            Stats a = new Stats(
                b.Health + c.Health,
                b.Attack + c.Attack,
                b.Defense + c.Defense,
                b.Speed + c.Speed,
                b.Special + c.Special
            );
            return a;
        }

        public static Stats operator- (Stats b, Stats c)
        {
            Stats a = new Stats(
                b.Health - c.Health,
                b.Attack - c.Attack,
                b.Defense - c.Defense,
                b.Speed - c.Speed,
                b.Special - c.Special
            );
            return a;
        }

        public static Stats operator* (Stats b, Stats c)
        {
            Stats a = new Stats(
                b.Health * c.Health,
                b.Attack * c.Attack,
                b.Defense * c.Defense,
                b.Speed * c.Speed,
                b.Special * c.Special
            );
            return a;
        }
    }
}
