using System.Collections.Generic;

namespace Buffs
{
    public readonly struct Data
    {
        public readonly int Id;
        public readonly string Name;
        public readonly HashSet<Data> PrecedenceOver;  // this buff will remove any buffs in this set.
        public readonly HashSet<Data> MutuallyExclusive; // this buff cannot coexist with any buffs in this set.
    }
}
