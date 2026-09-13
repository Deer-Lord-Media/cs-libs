using System;

namespace Quest.Data
{
    public readonly struct Stage
    {
        public readonly Guid eventId;
        public readonly int number;
        public readonly string description;

        public Stage(Guid eventId, int number, string description)
        {
            this.eventId = eventId;
            this.number = number;
            this.description = description;
        }
    }
}
