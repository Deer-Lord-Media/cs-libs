using System;

namespace CSLibs.Inventory
{
    public class Data : IEquatable<Data>
    { 
        public Data(int id, string name, string description = "", bool unique = false, int maxStacks = 1, int maxUses = 0, TimeSpan coolDown = TimeSpan.Zero)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Unique = unique;
            this.MaxStacks = maxStacks;
            this.MaxUses = maxUses;
            this.CoolDown = coolDown;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool Unique { get; }
        public int MaxStacks { get; }
        public int MaxUses { get; }
        public TimeSpan CoolDown { get; }

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
