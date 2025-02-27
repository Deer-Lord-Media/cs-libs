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
        public Action Action {get; set; }
    }
}

/*

TODO: write a timer that makes expiry go off after duration.
expiry message should be cancelled if using Start() or Clear().

*/
