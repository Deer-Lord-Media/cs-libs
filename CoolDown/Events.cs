using System;

namespace CoolDown
{
    public class Events : IPublisher, ISubscriber
    {
        private event EventHandler<Message>? Notify;

        public void Subscribe(EventHandler<Message> listener)
        {
            this.Notify += listener;
        }

        public void Unsubscribe(EventHandler<Message> listener)
        {
            this.Notify -= listener;
        }

        public void Publish(Action action, String id, DateTime startedAt, TimeSpan duration)
        {
            var message = new Message
            {
                Action = action,
                Id = id,
                StartedAt = startedAt,
                Duration = duration
            };
            Notify?.Invoke(this, message);
        }
    }
}
