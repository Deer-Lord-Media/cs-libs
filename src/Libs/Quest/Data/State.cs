using System;
using System.Collections.Generic;

namespace Quest.Data
{
    public readonly struct State
    {
        public readonly Guid encounterId;
        public readonly Dictionary<Guid, Comparator> comparators;
        public readonly bool outcome;
    }
}
