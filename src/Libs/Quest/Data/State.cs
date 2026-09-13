using System;
using System.Collections.Generic;

namespace Quest.Data
{
    // TODO: Rename this
    public readonly struct State
    {
        public readonly Guid id;
        public readonly Dictionary<Guid, Comparator> comparators;

    }
}
