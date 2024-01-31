using System;

namespace monsters
{
    public class Pokemon
    {
        public Pokemon()
        {
            // Tackle
            Move move = new Move(40, 100, MoveType.Normal, StatType.Attack);
            this.Moves[0] = move;
            this.Type1 = MoveType.Normal;

            BaseStats baseStats = new BaseStats(20, 10, 12, 18, 22);
            BaseStats ivs = new BaseStats(3, 3, 3, 3, 3);
            BaseStats evs = new BaseStats(0, 0, 0, 0, 0);
            MonsterStats permanentStats = new MonsterStats(1, baseStats, ivs, evs);
            this.PermanentStats = permanentStats;
            this.health = permanentStats.Health;
        }

        public MoveType? Type1 = null;
        public MoveType? Type2 = null;
        public MonsterStats PermanentStats;
        private BaseStats battleModifiers = new BaseStats(1, 1, 1, 1, 1);
        public Move[] Moves = new Move[4];
        private Random hitCheck = new Random();
        private int hitReduction = 0;
        private int health;
        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = Math.Clamp(value, 0, this.PermanentStats.Health);
            }
        }

        public BattleStats CurrentStats
        {
            get
            {
                var stats = new BattleStats(
                    (int)(this.PermanentStats.Health * this.battleModifiers.Health),
                    (int)(this.PermanentStats.Attack * this.battleModifiers.Attack),
                    (int)(this.PermanentStats.Defense * this.battleModifiers.Defense),
                    (int)(this.PermanentStats.Special * this.battleModifiers.Special),
                    (int)(this.PermanentStats.Speed * this.battleModifiers.Speed),
                    (int)(this.PermanentStats.SpecialAttack * this.battleModifiers.Special),
                    (int)(this.PermanentStats.SpecialDefense * this.battleModifiers.Special)
                );
                return stats;
            }
        }

        public bool UseMove(int index, Pokemon target)
        {
            bool retval = false;
            var move = this.Moves[index];

            // accuracy check
            if (hitCheck.Next() - hitReduction <= move.Accuracy)
            {
                int damage = move.Damage(this.CurrentStats, target.CurrentStats);
                if (this.Type1 == move.Type || this.Type2 != null && this.Type2 == move.Type)
                {
                    damage = (int)(damage * 1.5);
                }   
                target.TakeDamage(damage);
                retval = true;
            }
            return retval;
        }

        public void TakeDamage(int damage)
        {
            // check for other mitigations
            this.Health -= damage;
        }
    }
}
