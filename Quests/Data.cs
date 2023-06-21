using System;

namespace CSLibs.Quests
{

    public class Data : IEquatable<Data>
    {
        public Data(int id, string name, string description, int totalStages = 1)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.TotalStages = totalStages;
        }

        public int Id { get; }
        public string Name { get;} 
        public string Description { get; }
        public int TotalStages { get; }

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
