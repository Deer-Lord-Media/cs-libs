using System;

namespace monsters
{
    public class Pokemon
    {
        public Pokemon(MonsterStats permanentStats, Element type1, Element? type2 = null, Move[]? moves = null)
        {
            this.PermanentStats = permanentStats;
            this.Moves = (moves == null) ? new Move[4] : moves;
            this.health = permanentStats.Health;
            this.Type1 = type1;
            this.Type2 = type2;
        }

        public Element Type1;
        public Element? Type2 = null;
        public MonsterStats PermanentStats;
        private BaseStats battleModifiers = new BaseStats(1, 1, 1, 1, 1);
        public Move[] Moves = new Move[4];
        public int HitReduction = 0;
        private int health;
        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = Math.Clamp(value, 0, this.CurrentStats.Health);
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

        public int UseMove(int index, Pokemon target)
        {
            int damage = 0;
            Move move = this.Moves[index];

            if (move.Hit(this, target))
            {
                damage = move.Damage(this, target);
                // this can probably be more robust
                target.Health -= damage;
            }
            return damage;
        }
    }
}
