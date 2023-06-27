using System;

namespace CSLibs.Items
{
    public class CoolDown
    {
        public CoolDown(TimeSpan duration)
        {
            this.Duration = duration;
        }
        public DateTime StartedAt { get; }
        public TimeSpan Duration { get; }

        public bool Start()
        {
            bool retval = false;
            if (this.Ready)
            {
                this.StartedAt = DateTime.Now;
                retval = true;
            }
            return retval;
        }

        public bool OnCoolDown
        {
            get
            {
                return DateTime.Now - this.StartedAt < this.Duration;
            }
        }

        public bool Ready
        {
            get
            {
                return DateTime.Now - this.StartedAt > this.Duration;
            }
        }
    }
}