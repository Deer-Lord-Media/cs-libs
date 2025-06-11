using System;

namespace CoolDown
{
    public enum Action
    {
        Start,
        Expire
    }

    public class Message : EventArgs
    {
        public readonly Guid Id;
        public readonly Action Action;
        public readonly DateTime StartedAt;
        public readonly TimeSpan Duration;

        public Message(Guid id, Action action, DateTime startedAt, TimeSpan duration)
        {
            this.Id = id;
            this.Action = action;
            this.StartedAt = startedAt;
            this.Duration = duration;
        }
    }
}
