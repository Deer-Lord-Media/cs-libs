using System;

namespace CSLibs.Items
{
    public class Slot
    {
        public Slot(Item item, int number, int? stacks = null)
        {
            this.Item = item;
            this.Number = number;
            if (stacks == null) stacks = 1;
            this.Stacks = Math.Max(0, Math.Min(item.Data.MaxStacks, (int)stacks));
        }

        public int Number { get; }
        public Item Item { get; }
        public int Stacks { get; }

        public bool IsFull
        {
            get
            {
                return this.Stacks >= this.Item.Data.MaxStacks;
            }
        }

        public int FreeSpace
        {
            get
            {
                return this.Item.Data.MaxStacks - this.Stacks;
            }
        }
    }
}
