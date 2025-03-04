using System;

namespace CoolDown
{
    public enum Action
    {
        Start,
        Refresh,
        Expire
    }

    public class Message : EventArgs
    {
        public string Id { get; set; }
        public Action Action { get; set; }
        public DateTime StartedAt { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public interface IPublisher
    {
        void Publish(Action action, String id, DateTime startedAt, TimeSpan duration);
    }

    public interface ISubscriber
    {
        void Subscribe(EventHandler<Message> listener);
        void Unsubscribe(EventHandler<Message> listener);
    }

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
