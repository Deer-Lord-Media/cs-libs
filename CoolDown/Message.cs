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
        public string Id { get; }
        public Action Action { get; }
        public DateTime StartedAt { get; }
        public TimeSpan Duration { get; }

        public Message(string id, Action action, DateTime startedAt, TimeSpan duration)
        {
            this.Id = id;
            this.Action = action;
            this.StartedAt = startedAt;
            this.Duration = duration;
        }
    }
}
