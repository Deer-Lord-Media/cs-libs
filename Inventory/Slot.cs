using System;

namespace CSLibs.Inventory
{
    public class Slot
    {
        public Slot(Data data, int number, int? stacks = null, int? uses = null)
        {
            this.Data = data;
            this.Number = number;
            if (stacks == null) stacks = 1;
            this.Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
            if (uses == null) uses = 0;
            this.Uses = Math.Max(0, Math.Min(data.MaxUses, uses));
        }

        public int Number { get; }
        public Data Data { get; }
        public int Stacks { get; }
        public int Uses { get; }

        public bool IsFull
        {
            get
            {
                return this.Stacks >= this.Data.MaxStacks;
            }
        }
    }
}