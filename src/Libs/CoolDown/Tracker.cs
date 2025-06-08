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

        /// <summary>
        /// Returns true if the cooldown is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.TimeRemaining > TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Returns how much time is left on the active cooldown.
        /// </summary>
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

        /// <summary>
        /// Starts the cooldown.
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            if (this.IsActive)
            {
                return false;
            }
            this.startedAt = DateTime.Now;
            this.publisher.Publish(this.id, Action.Start, this.startedAt, this.duration);
            return this.PendExpiry(this.TimeRemaining);
        }

        /// <summary>
        /// Forcefully expires the cooldown.
        /// </summary>
        private void Expire()
        {
            this.startedAt = DateTime.Now - this.duration;
            this.publisher.Publish(this.id, Action.Expire, this.startedAt, this.duration);
        }

        /// <summary>
        /// Clears the cooldown and it's pending expiration message.
        /// </summary>
        /// <returns>Success of the operation.</returns>
        public bool Clear()
        {
            if (!this.IsActive)
            {
                return true;
            }
            this.CancelExpiry();
            this.Expire();
            return true;
        }

        /// <summary>
        /// Creates a pending message that fires when the cooldown expires.
        /// </summary>
        /// <param name="duration">How long to wait until the event is fired.</param>
        /// <returns>Success of creating the event.</returns>
        private bool PendExpiry(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                return false;
            }

            // cancel any pending expiry message
            this.CancelExpiry();
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(duration, this.cancel.Token);
                    this.Expire();
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            });

            return true;
        }

        /// <summary>
        /// Cancels any existing expiry message.
        /// </summary>
        private void CancelExpiry()
        {
            this.cancel.Cancel();
            this.cancel = new CancellationTokenSource();
        }
    }
}
