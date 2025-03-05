using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoolDown
{
    public class Tracker
    {
        public Tracker(TimeSpan duration, int id, Events events)
        {
            this.duration = duration;
            this.startedAt = DateTime.Now - duration;
            this.id = id;
            this.cancel = new CancellationTokenSource();
            this.subscriber = events;
            this.publisher = events;
        }

        public readonly int id;
        private DateTime startedAt;
        public DateTime StartedAt { get { return this.startedAt; } }
        public readonly TimeSpan duration;
        private CancellationTokenSource cancel;
        public readonly ISubscriber subscriber;
        private readonly IPublisher publisher;

        public TimeSpan TimeRemaining
        {
            get
            {
                var timeRemaining = (this.startedAt + this.duration) - DateTime.Now;
                if (timeRemaining < TimeSpan.Zero)
                {
                    timeRemaining = TimeSpan.Zero;
                }
                return timeRemaining;
            }
        }

        public bool IsReady
        {
            get
            {
                return this.TimeRemaining == TimeSpan.Zero;
            }
        }

        public bool Start()
        {
            if (!this.IsReady)
            {
                return false;
            }
            this.startedAt = DateTime.Now;
            this.publisher.Publish(this.id, Action.Start, this.startedAt, this.duration);
            return this.PendExpiry(this.TimeRemaining);
        }

        public bool Refresh()
        {
            if (this.IsReady)
            {
                return false;
            }
            this.startedAt = DateTime.Now;
            this.publisher.Publish(this.id, Action.Start, this.startedAt, this.duration);
            return this.PendExpiry(this.TimeRemaining);
        }

        public bool Clear()
        {
            if (this.IsReady)
            {
                return false;
            }
            this.startedAt = DateTime.Now - this.duration;
            this.CancelExpiry();
            this.publisher.Publish(this.id, Action.Expire, this.startedAt, this.duration);
            return true;
        }

        private bool PendExpiry(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                return false;
            }

            this.CancelExpiry();
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(duration, this.cancel.Token);
                    this.Expire();
                    this.publisher.Publish(this.id, Action.Expire, this.StartedAt, this.duration);  // Fire the event, replace with your event logic
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            });

            return true;
        }

        private void Expire()
        {
            this.startedAt = DateTime.Now - this.duration;
            this.publisher.Publish(this.id, Action.Start, this.startedAt, this.duration);
        }

        private void CancelExpiry()
        {
            this.cancel.Cancel();
            this.cancel = new CancellationTokenSource();
        }
    }
}
