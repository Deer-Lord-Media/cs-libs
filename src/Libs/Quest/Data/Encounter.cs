using System;

namespace Quest.Data
{
    public readonly struct Encounter
    {
        public readonly Guid id;
        public readonly string name;
        public readonly string description;
        public readonly State[] states;

        public Encounter(Guid id, string name, string description, State[] states)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.states = states;
        }
    }
}
