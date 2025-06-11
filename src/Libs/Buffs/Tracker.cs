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
                if (current.coolDown.Start())
                {
                    if (current.Increment())
                        return Action.Incremented;
                    return Action.Refreshed;
                }
                return Action.Failed;
            }

            // buff can't be applied because it is mutually exclusive or weaker than existing buff
            if (active.Where(m => m.data == buff.data || m.data > buff.data).FirstOrDefault() != null)
                return Action.Blocked;

            // remove weaker buffs
            foreach (Buff toRemove in active.Where(m => m.data < buff.data))
                if (toRemove.coolDown.Clear())
                    this.members.Remove(toRemove);

            // no room for buffs
            if (members.Count >= this.maxMembers)
                return Action.Full;

            // add buff and register handler
            if (!this.members.Add(buff))
                return Action.Failed;

            if (buff.coolDown.Start())
            {
                buff.coolDown.subscriber.Subscribe(this.HandleExpiry);
                return Action.Applied;
            }
            return Action.Failed;
        }

        public Action Remove(Data data)
        {
            var active = new HashSet<Buff>(this.Active);
            var current = active.Where(m => m.Equals(data)).FirstOrDefault();
            if (current != null)
            {
                if (current.Decrement())
                    return Action.Decremented;
                var toRemove = active.Where(m => m.data == data).FirstOrDefault();
                if (toRemove != null)
                {
                    if (this.members.Remove(toRemove))
                        return Action.Removed;
                }
            }
            return Action.Failed;
        }

        public bool Clear()
        {
            this.members.Clear();
            return this.members.Count == 0;
        }

        private void HandleExpiry(object? sender, CoolDown.Message e)
        {
            if (e.Action != CoolDown.Action.Expire)
                return;

            foreach (Buff buff in this.members.Where(m => m.coolDown.id == e.Id))
            {
                buff.coolDown.subscriber.Unsubscribe(this.HandleExpiry);
                this.members.Remove(buff);
            }
        }
    }
}
