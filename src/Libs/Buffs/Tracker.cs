using System;
using System.Collections.Generic;
using System.Linq;

namespace Buffs
{
    public class Tracker
    {
        public Tracker(int maxMembers)
        {
            this.maxMembers = maxMembers;
        }

        private HashSet<Buff> members = new HashSet<Buff>();
        private int maxMembers;

        public IEnumerable<Buff> Active
        {
            get
            {
                return this.members.Where(m => m.coolDown.IsActive);
            }
        }

        public Action Apply(Buff buff)
        {
            var active = new HashSet<Buff>(this.Active);

            // refresh buff if it's already applied and active
            var current = active.Where(m => m.data.Equals(buff.data)).FirstOrDefault();
            if (current != null)
            {
                if (current.coolDown.Start()) return Action.Refreshed;
                return Action.Noop;
            }

            // buff can't be applied because it is mutually exclusive or weaker
            if (active.Where(m => m.data == buff.data || m.data > buff.data).FirstOrDefault() != null) return Action.Blocked;

            // remove weaker buffs
            foreach (Buff toRemove in active.Where(m => m.data < buff.data))
            {
                if (toRemove.coolDown.Clear()) this.members.Remove(toRemove);
            }

            // no room for buffs
            if (members.Count >= this.maxMembers) return Action.Full;

            // add buff and register handler
            if (!this.members.Add(buff))
            {
                return Action.Noop;
            }
            buff.coolDown.subscriber.Subscribe(this.HandleExpiry);
            return Action.Applied;
        }

        private void HandleExpiry(object? sender, CoolDown.Message e)
        {
            if (e.Action != CoolDown.Action.Expire)
            {
                return;
            }

            foreach (Buff buff in this.members.Where(m => m.coolDown.id == e.Id))
            {
                buff.coolDown.subscriber.Unsubscribe(this.HandleExpiry);
                this.members.Remove(buff);
            }
        }
    }
}
