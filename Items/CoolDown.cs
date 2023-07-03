using System;

namespace CSLibs.Items
{
    public class CoolDown
    {
        public CoolDown(TimeSpan duration)
        {
            this.Duration = duration;
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

            if (this.OnCoolDown)
            {
                this.startedAt = DateTime.Now - this.Duration;
                retval = true;
            } else
            {
                retval = false;
            }
            return retval;
        }

        public bool OnCoolDown
        {
            get
            {
                return DateTime.Now - this.StartedAt <= this.Duration;
            }
        }

        public bool Ready
        {
            get
            {
                return !this.OnCoolDown;
            }
        }
    }
}
