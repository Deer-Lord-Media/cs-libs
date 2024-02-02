using System.ComponentModel;

namespace monsters
{
    public struct BattleStats
    {
        public BattleStats(int health, int attack, int defense, int special, int speed, int specialAttack, int specialDefense)
        {
            this.Health = health;
            this.Attack = attack;
            this.Defense = defense;
            this.Special = special;
            this.Speed = speed;
            this.SpecialAttack = specialAttack;
            this.SpecialDefense = specialDefense;
        }
        public int Health;
        public int Attack;
        public int Defense;
        public int Special;
        public int Speed;
        public int SpecialAttack;
        public int SpecialDefense;
    }
}