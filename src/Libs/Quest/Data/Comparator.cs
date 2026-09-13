using System;

namespace Quest.Data
{

    public readonly struct Comparator
    {
        public readonly Guid id;
        public readonly Guid againstId;
        public readonly Against againstType;
        public readonly string symbol;
        public readonly int value;

        public bool Compare(int other)
        {
            var func = Operator.Function(symbol);
            return func != null && func(value, other);
        }
    }
}
