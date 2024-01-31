using System;
using System.Collections.Generic;
using System.Linq;

namespace CSLibs.Buffs
{
    public class Data : IEquatable<Data>
    {
        public Data(
            int id,
            string name,
            TimeSpan duration,
            HashSet<int>? similarTo = null,
            HashSet<int>? precedenceOver = null,
            int priorityLevel = 0,
            int maxStacks = 1
        )
        {
            this.Id = id;
            this.Name = name;
            this.Duration = duration;
            this.similarTo = (similarTo == null)
                ? new HashSet<int>()
                : new HashSet<int>(similarTo);
            this.precedenceOver = (precedenceOver == null)
                ? new HashSet<int>()
                : new HashSet<int>(precedenceOver);
            this.PriorityLevel = priorityLevel;
            this.MaxStacks = maxStacks;
        }

        public int Id { get; }
        public string Name { get; }
        public TimeSpan Duration { get; }
        public int PriorityLevel { get; }
        public int MaxStacks { get; }
        private HashSet<int> similarTo { get; }
        private HashSet<int> precedenceOver { get; }

        public bool IsSimilarTo(Data data)
        {
            return (this.similarTo.Count() > 0) && this.similarTo.Contains(data.Id);
        }

        public bool HasPrecedenceOver(Data data)
        {
            return (this.precedenceOver.Count() > 0) && this.precedenceOver.Contains(data.Id);
        }

        public bool HasPriorityOver(Data data)
        {
            return this.PriorityLevel >= data.PriorityLevel;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

        public bool Equals(Data data)
        {
            return this.Id == data.Id;
        }
    }
}
