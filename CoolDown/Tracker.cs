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
        private bool pendingCancellation;
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
            var timePassed = this.duration - (DateTime.Now - this.startedAt);
            this.PendEvent(Action.Expire, timePassed);
            return;
        }

        public void Refresh()
        {
            this.startedAt = DateTime.Now;
            this.ActionOccurred(Action.Refresh);
            this.CancelPending();
            var timePassed = this.duration - (DateTime.Now - this.startedAt);
            this.PendEvent(Action.Expire, timePassed);
        }

        public void Clear()
        {
            if (this.IsReady)
            {
                return;
            }
            this.startedAt = DateTime.Now - this.Duration;
            this.CancelPending();
            this.ActionOccurred(Action.Expire);
            return;
        }

        private void PendEvent(Action action, TimeSpan duration)
        {
            this.pendingCancellation = true;
            // timer to wait
            this.ActionOccurred(action);
        }

        private void CancelPending()
        {
            this.pendingCancellation = false;
        }
    }
}
