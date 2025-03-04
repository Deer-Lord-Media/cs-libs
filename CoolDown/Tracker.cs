using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoolDown
{
    public class Tracker
    {
        public Tracker(TimeSpan duration, string id)
        {
            this.duration = duration;
            this.startedAt = DateTime.Now - duration;
            this.id = id;
            this.cancel = new CancellationTokenSource();
        }

        public readonly string id;
        private DateTime startedAt;
        public DateTime StartedAt { get { return this.startedAt; } }
        public readonly TimeSpan duration;
        private event EventHandler<Message>? Notify;
        private CancellationTokenSource cancel;
        public void Register(EventHandler<Message> listener)
        {
            this.Notify += listener;
        }

        public void Deregister(EventHandler<Message> listener)
        {
            this.Notify -= listener;
        }

        protected virtual void SendEvent(Action action)
        {
            var message = new Message
            {
                Action = action,
                Id = this.id
            };
            Notify?.Invoke(this, message);
        }

        public TimeSpan TimeRemaining
        {
            get
            {
                var timeRemaining = (this.startedAt + this.duration) - DateTime.Now;
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
                return this.TimeRemaining == new TimeSpan(0);
            }
        }

        public bool Start()
        {
            if (!this.IsReady)
            {
                return false;
            }
            this.startedAt = DateTime.Now;
            this.SendEvent(Action.Start);
            this.PendExpiry(this.TimeRemaining);
            return true;
        }

        public bool Refresh()
        {
            if (this.IsReady)
            {
                return false;
            }
            this.startedAt = DateTime.Now;
            this.SendEvent(Action.Refresh);
            this.PendExpiry(this.TimeRemaining);
            return true;
        }

        public bool Clear()
        {
            if (this.IsReady)
            {
                return false;
            }
            this.startedAt = DateTime.Now - this.duration;
            this.CancelExpiry();
            this.SendEvent(Action.Expire);
            return true;
        }

        private void PendExpiry(TimeSpan duration)
        {
            if (duration <= new TimeSpan(0))
            {
                return;
            }

            this.CancelExpiry();
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(duration.Milliseconds, this.cancel.Token);
                    this.Expire();
                    this.SendEvent(Action.Expire);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            });
        }

        private void Expire()
        {
            this.startedAt = DateTime.Now - this.duration;
        }

        private void CancelExpiry()
        {
            this.cancel.Cancel();
            this.cancel = new CancellationTokenSource();
        }
    }
}
