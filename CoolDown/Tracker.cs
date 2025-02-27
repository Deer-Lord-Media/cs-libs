using System;

namespace CoolDown
{
    public class Tracker
    {
        public Tracker(TimeSpan duration, string id)
        {
            this.duration = duration;
            this.startedAt = DateTime.Now - duration;
            this.id = id;
        }

        private event EventHandler<Message>? Notify;
        private readonly string id;
        private DateTime startedAt;
        public DateTime StartedAt { get { return this.startedAt; } }
        private readonly TimeSpan duration;
        public TimeSpan Duration { get { return this.duration; } }

        public void RegisterListener(EventHandler<Message> listener)
        {
            this.Notify += listener;
        }

        protected virtual void ActionOccurred(Action action)
        {
            var message = new Message();
            message.Action = action;
            message.Id = this.id;

            Notify?.Invoke(this, message);
        }

        public TimeSpan timeRemaining {
            get 
            {
                var timeRemaining = (this.startedAt + this.Duration) - DateTime.Now;
                if (timeRemaining < new TimeSpan(0))
                {
                    timeRemaining = new TimeSpan(0);
                }
                return timeRemaining;
            }
        }

        public bool IsReady
        {
            get
            {
                return this.timeRemaining.Milliseconds <= 0;
            }
        }

        public void Start()
        {            
            this.startedAt = DateTime.Now;
            this.ActionOccurred(Action.Start);
            /*
                TODO: set a timer
            */
            return;
        }

        public void Clear()
        {
            if (this.IsReady)
            {
                return;
            }
            /*
                TODO: clear the timer
            */
            this.startedAt = DateTime.Now - this.Duration;
            this.ActionOccurred(Action.Expire);
            return;
        }
    }
}
