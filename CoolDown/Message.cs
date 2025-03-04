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
}
