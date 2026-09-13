using System;

namespace Quest.Data
{
    public readonly struct Encounter
    {
        public readonly Guid id;
        public readonly string name;
        public readonly string description;
        public readonly Outcome[] outcomes;

        public Encounter(Guid id, string name, string description, Outcome[] outcomes)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.outcomes = outcomes;
        }
    }
}
