using System;

namespace CSLibs.Inventory
{
    public class Data : IEquatable<Data>
    { 
        public Data(int id, bool unique = false, int maxStacks = 1)
        {
            this.Id = id;
            this.Unique = unique;
            this.MaxStacks = maxStacks;
        }

        public int Id {get; }
        public bool Unique {get; }
        public int MaxStacks {get; }

        public override int GetHashCode()
        {
            return this.Id;
        }

        public bool Equals(Data obj)
        {
            return this.Id == obj.Id;
        }
    }
}
