using System;

namespace CoolDown
{
    public class Events : IPublisher, ISubscriber
    {
        private event EventHandler<Message>? Notify;

        public void Subscribe(EventHandler<Message> subscriber)
        {
            this.Notify += subscriber;
        }

        public void Unsubscribe(EventHandler<Message> subscriber)
        {
            this.Notify -= subscriber;
        }

        public void Publish(Guid id, Action action, DateTime startedAt, TimeSpan duration)
        {
            var message = new Message(
                id,
                action,
                startedAt,
                duration
            );
            Notify?.Invoke(this, message);
        }
    }
}
