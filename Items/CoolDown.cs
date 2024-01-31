using System;

namespace CSLibs.Items
{
    public class CoolDown
    {
        public CoolDown(TimeSpan duration)
        {
            this.Duration = duration;
            this.startedAt = DateTime.Now - duration;
        }
        private DateTime startedAt;
        public DateTime StartedAt { get { return this.startedAt; } }
        public TimeSpan Duration { get; }

        public bool Start()
        {
            bool retval = false;
            if (this.Ready)
            {
                this.startedAt = DateTime.Now;
                retval = true;
            }
            return retval;
        }

        public bool Reset()
        {
            bool retval;

            if (this.Busy)
            {
                this.startedAt = DateTime.Now - this.Duration;
                retval = true;
            } else
            {
                retval = false;
            }
            return retval;
        }

        public bool Busy
        {
            get
            {
                return DateTime.Now - this.Duration < this.startedAt;
            }
        }

        public bool Ready
        {
            get
            {
                return !this.Busy;
            }
        }
    }
}
