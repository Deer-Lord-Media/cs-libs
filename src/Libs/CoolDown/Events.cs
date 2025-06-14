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

        /// <summary>
        /// Publish a cooldown event.
        /// </summary>
        /// <param name="id">The id of the cooldown.</param>
        /// <param name="action">The action taken by the cooldown.</param>
        /// <param name="startedAt">The time the cooldown started.</param>
        /// <param name="duration">How long the cooldown lasts.</param>
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
