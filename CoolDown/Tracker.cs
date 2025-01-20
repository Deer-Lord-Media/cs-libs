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
        public TimeSpan TimeRemaining {
            get 
            {
                var timeRemaining = (this.startedAt + this.Duration) - DateTime.Now;
                return timeRemaining;
            }
        }

        public bool Start()
        {
            bool retval = false;
            if (this.IsReady)
            {
                this.startedAt = DateTime.Now;
                retval = true;
            }
            return retval;
        }

        public bool Reset()
        {
            this.startedAt = DateTime.Now - this.Duration;
            return true;
        }

        public bool IsReady
        {
            get
            {
                return this.TimeRemaining.Milliseconds <= 0;
            }
        }
    }
}
