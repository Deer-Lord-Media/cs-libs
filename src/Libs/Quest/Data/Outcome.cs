using System;
using System.Collections.Generic;

namespace Quest.Data
{
    public readonly struct Outcome
    {
        public readonly Guid id;
        public readonly Dictionary<Guid, Comparator> comparators;

    }
}
