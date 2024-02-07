using System;

namespace Pokemon
{

    public class CoolDown
    {
        public CoolDown()
        {
            this.duration = TimeSpan.Zero;
        }

        public DateTime StartedAt
        {
            get
            {
                return startedAt;
            }
        }
        private DateTime startedAt;

        public TimeSpan Duration
        {
            get
            {
                return duration;
            }
        }
        private TimeSpan duration;

        public bool Start(TimeSpan duration)
        {
            bool retval = false;
            if (this.Ready)
            {
                this.duration = duration;
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
                this.startedAt = DateTime.Now;
                this.duration = TimeSpan.Zero;
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
                return DateTime.Now - this.duration < this.startedAt;
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
