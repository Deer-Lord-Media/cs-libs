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

    public interface ISender
    {
        void SendEvent(Action action, String id, DateTime startedAt, TimeSpan duration);
    }

    public interface IReceiver
    {
        void Register(EventHandler<Message> listener);
        void Deregister(EventHandler<Message> listener);
    }

    public class Events : ISender, IReceiver
    {
        private event EventHandler<Message>? Notify;

        public void Register(EventHandler<Message> listener)
        {
            this.Notify += listener;
        }

        public void Deregister(EventHandler<Message> listener)
        {
            this.Notify -= listener;
        }

        public void SendEvent(Action action, String id, DateTime startedAt, TimeSpan duration)
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
