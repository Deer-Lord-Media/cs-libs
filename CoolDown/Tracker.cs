using System;

namespace CoolDown
{
    public class Tracker
    {
        public Tracker(TimeSpan duration)
        {
            this.duration = duration;
            this.startedAt = DateTime.Now - duration;
        }
        private DateTime startedAt;
        public DateTime StartedAt { get { return this.startedAt; } }
        private TimeSpan duration;
        public TimeSpan Duration { get { return this.duration; } }
    }
}
