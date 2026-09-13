using System;

namespace Quest.Data
{
    public readonly struct Knowledge
    {
        public readonly Guid knowerId;  // Person who knows
        public readonly Guid knowsId;   // that someone else knows
        public readonly int value;      // a detail
        public readonly Guid eventId;   // about something

        public Knowledge(Guid knowerId, Guid knowsId, int value, Guid eventId)
        {
            this.knowerId = knowerId;
            this.knowsId = knowsId;
            this.value = value;
            this.eventId = eventId;
        }
    }
}
