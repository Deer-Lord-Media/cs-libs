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

        public Buff? Get(Data data)
        {
            return this.Active.Where(m => m.data.Equals(data)).FirstOrDefault();
        }

        /// <summary>
        /// Enumerable of the currently active buffs.
        /// </summary>
        public IEnumerable<Buff> Active
        {
            get
            {
                return this.members.Where(m => m.coolDown.IsActive && m.stacks.Value > 0);
            }
        }

        /// <summary>
        /// Apply a buff.
        /// </summary>
        /// <param name="buff">An existing instance of a Buff to apply.</param>
        /// <returns></returns>
        public Action Apply(Buff buff)
        {
            return this.Apply(buff.data, buff.coolDown);
        }

        /// <summary>
        /// Apply a buff.
        /// </summary>
        /// <param name="data">The data to create the Buff from.</param>
        /// <param name="coolDown">An optional coolDown to apply the Buff with.</param>
        /// <returns>The action that the application was able to perform.</returns>
        public Action Apply(Data data, CoolDown.Tracker? coolDown)
        {
            var active = new HashSet<Buff>(this.Active);

            // refresh buff if it's already applied and active
            var current = this.Get(data);
            if (current != null)
            {
                if (current.coolDown.Start())
                {
                    if (current.stacks.Increment())
                        return Action.Incremented;
                    return Action.Refreshed;
                }
                return Action.Failed;
            }

            // cannot create Buff if coolDown is missing
            if (coolDown == null)
                return Action.CoolDownMissing;

            var buff = new Buff(data, coolDown);
            buff.stacks.Increment();

            // buff can't be applied because it is mutually exclusive or weaker than existing buff
            if (active.Where(m => m.data == data || m.data > data).FirstOrDefault() != null)
                return Action.Blocked;

            // remove weaker buffs
            foreach (Buff toRemove in active.Where(m => m.data < data))
                this.Delete(toRemove);

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

        /// <summary>
        /// Remove a stack from an existing buff, and delete the Buff if necessary.
        /// </summary>
        /// <param name="data">Data to find the Buff.</param>
        /// <returns>The action taken.</returns>
        public Action Remove(Data data)
        {
            // Get the existing buff from the set.
            var buff = this.Get(data);
            if (buff != null)
            {
                // Decrement the stacks
                if (buff.stacks.Decrement())
                {
                    // Remove the buff is there are no stacks.
                    if (buff.stacks.Value == 0)
                    {
                        this.Delete(buff);
                        return Action.Removed;
                    }
                    // Stack count decremented.
                    return Action.Decremented;
                }
            }
            return Action.Failed;
        }

        /// <summary>
        /// Clear a specific Buff.
        /// </summary>
        /// <param name="data">Data representing the Buff to clear.</param>
        /// <returns>Success of clearing the member(s).</returns>
        public bool Clear(Data data)
        {
            // If a Buff is provided, unsubscribe from handling expiry and remove the Buff.
            var buff = this.Get(data);
            if (buff == null)
                return false;
            this.Delete(buff);
            return true;
        }

        public bool Empty()
        {
            var members = new HashSet<Buff>(this.members);
            foreach (Buff toRemove in members)
                this.Delete(toRemove);

            this.members.Clear();
            return this.members.Count == 0;
        }

        private void Delete(Buff buff)
        {
            buff.stacks.Empty();
            buff.coolDown.subscriber.Unsubscribe(this.HandleExpiry);
            this.members.Remove(buff);
        }

        /// <summary>
        /// Handler for coolDown expiration messages.
        /// </summary>
        /// <param name="sender">Sender of the message. Unused.</param>
        /// <param name="e">The message to handle.</param>
        private void HandleExpiry(object? sender, CoolDown.Message e)
        {
            if (e.Action != CoolDown.Action.Expire)
                return;

            foreach (Buff buff in this.members.Where(m => m.coolDown.id == e.Id))
            {
                this.Delete(buff);
            }
        }
    }
}
