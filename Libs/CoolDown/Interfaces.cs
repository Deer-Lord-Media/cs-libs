using System;

namespace CoolDown
{
    public interface IPublisher
    {
        void Publish(String id, Action action, DateTime startedAt, TimeSpan duration);
    }

    public interface ISubscriber
    {
        void Subscribe(EventHandler<Message> handler);
        void Unsubscribe(EventHandler<Message> handler);
    }
}
