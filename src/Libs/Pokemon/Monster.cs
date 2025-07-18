using System;
using Resource;
using Buffs;
namespace Pokemon
{
    public class Monster
    {
        public readonly string name;
        private Stats.Container stats;
        public readonly Move[] moves = new Move[4];
    }
}
