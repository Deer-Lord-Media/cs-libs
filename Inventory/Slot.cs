using System;

namespace Inventory
{
    public class Slot
    {
        public Slot(Data data, int number, int stacks = 1)
        {
            this.Data = data;
            this.Number = number;
            this.Stacks = Math.Max(0, Math.Min(data.MaxStacks, stacks));
        }

        public int Number { get; }
        public Data Data { get; }
        public int Stacks { get; }

        public bool IsFull
        {
            get
            {
                return this.Stacks >= this.Data.MaxStacks;
            }
        }
    }
}