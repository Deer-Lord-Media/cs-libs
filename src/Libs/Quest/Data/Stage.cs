using System;

namespace Quest.Data
{
    public readonly struct Stage
    {
        public readonly Guid id;
        public readonly int number;
        public readonly string description;

        public Stage(int number, string description, Guid id)
        {
            this.id = id;
            this.number = number;
            this.description = description;
        }
    }
}
