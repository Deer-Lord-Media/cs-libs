namespace Pokemon.Stats
{
    public readonly struct Basic
    {
        public Basic(
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
        public readonly int Health;
        public readonly int Attack;
        public readonly int Defense;
        public readonly int Speed;
        public readonly int Special;

        public readonly int SpcAttack
        {
            get
            {
                return (int)((this.Attack + this.Special) / 2);
            }
        }

        public readonly int SpcDefense
        {
            get
            {
                return (int)((this.Defense + this.Special) / 2);
            }
        }

        public static Basic operator+ (Basic b, Basic c)
        {
            Basic a = new Basic(
                b.Health + c.Health,
                b.Attack + c.Attack,
                b.Defense + c.Defense,
                b.Speed + c.Speed,
                b.Special + c.Special
            );
            return a;
        }

        public static Basic operator- (Basic b, Basic c)
        {
            Basic a = new Basic(
                b.Health - c.Health,
                b.Attack - c.Attack,
                b.Defense - c.Defense,
                b.Speed - c.Speed,
                b.Special - c.Special
            );
            return a;
        }
    }
}
