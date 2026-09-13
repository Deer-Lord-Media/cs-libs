using System;

namespace Quest.Data
{
    public readonly struct Event
    {
        public readonly Guid id;
        public readonly Stage[] stages;

        public readonly int count;

        public Event(Stage[] stages, Guid id)
        {
            this.id = id;
            this.stages = stages;
            count = stages.Length;
        }
    }
}
