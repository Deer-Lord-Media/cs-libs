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
                return this.members.Where(m => m.coolDown.TimeRemaining > TimeSpan.Zero);
            }
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

        public bool Apply(Buff buff)
        {
            var active = new HashSet<Buff>(this.Active);

            // refresh buff if it's already applied and active
            var exact = active.Where(m => m.data.Id == buff.data.Id).FirstOrDefault();
            if (exact != null) return exact.coolDown.Refresh();

            // buff can't be applied because it is mutually exclusive or weaker
            var excluded = active.Where(m => m == buff || m > buff).FirstOrDefault();
            if (excluded != null) return false;

            // remove weaker buffs
            foreach (Buff toRemove in active.Where(m => m < buff).ToArray())
            {
                if (toRemove.coolDown.Clear()) active.Remove(toRemove);
            }

            // exit if tracker is still full
            if (active.Count >= this.maxMembers) return false;

            if (!this.members.Add(buff))
            {
                buff.coolDown.Clear();
                return false;
            }
            buff.coolDown.subscriber.Subscribe(this.HandleExpiry);
            return true;
        }
    }
}
