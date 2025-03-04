using System;

namespace CoolDown
{
    public interface IPublisher
    {
        void Publish(Action action, String id, DateTime startedAt, TimeSpan duration);
    }

    public interface ISubscriber
    {
        void Subscribe(EventHandler<Message> listener);
        void Unsubscribe(EventHandler<Message> listener);
    }

}