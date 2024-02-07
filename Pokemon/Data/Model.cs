using System;

namespace Pokemon.Data
{
    public readonly struct Model
    {
        public Model(
            int id, Pokemon.Stats.Basic baseStats, Pokemon.Stats.Basic evs,
            Type type1, Type? type2 = null, int? nextId = null, int? prevId = null,
            ExperienceType expType = ExperienceType.MediumSlow
        )
        {
            this.Id = id;
            this.BaseStats = baseStats;
            this.Type1 = type1;
            this.Type2 = type2;
            this.NextId = nextId;
            this.PrevId = prevId;
            this.Experience = new Experience(expType);
            this.EVs = evs;
        }
        public readonly int Id;
        public readonly int? NextId;
        public readonly int? PrevId;
        public readonly Type Type1;
        public readonly Type? Type2;
        public readonly Experience Experience;
        public readonly Pokemon.Stats.Basic BaseStats;
        public readonly Pokemon.Stats.Basic EVs;
    }
}
