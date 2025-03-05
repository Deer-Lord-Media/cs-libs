using System.Collections.Generic;

namespace Buffs
{
    public readonly struct Data
    {
        public readonly int Id;
        public readonly string Name;
        public readonly HashSet<Data> PrecedenceOver;
        public readonly HashSet<Data> MutuallyExclusive;
    }
}
