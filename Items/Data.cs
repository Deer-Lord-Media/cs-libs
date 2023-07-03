using System;
using System.Collections.Generic;
using System.Linq;

namespace CSLibs.Items
{
    public class Data : IEquatable<Data>
    { 
        public Data(
            int id,
            string name,
            string? description = null,
            int maxStacks = 1,
            int maxCharges = 0,
            int maxOwned = 0,
            TimeSpan? coolDownDuration = null,
            HashSet<int>? sharesCoolDownWith = null
        )
        {
            this.Id = id;
            this.Name = name;
            this.Description = (description == null) ? "" : (string)description;
            this.MaxStacks = maxStacks;
            this.MaxCharges = maxCharges;
            this.MaxOwned = maxOwned;
            this.CoolDownDuration = (coolDownDuration == null) ? new TimeSpan(0, 0, 0, 0, 0) : (TimeSpan)coolDownDuration;
            this.sharesCoolDownWith = (sharesCoolDownWith == null)
                ? new HashSet<int>()
                : new HashSet<int>(sharesCoolDownWith);
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int MaxStacks { get; }
        public int MaxOwned { get; }
        public int MaxCharges { get; }
        public TimeSpan CoolDownDuration { get; }
        private HashSet<int> sharesCoolDownWith = new HashSet<int>();
        public bool IsUnique
        {
            get
            {
                return this.MaxOwned == 1;
            }
        }

        public bool SharesCoolDown(Data data)
        {
            return this.sharesCoolDownWith.Contains(data.Id);
        }

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
