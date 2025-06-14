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

        /// <summary>
        /// Get a Buff.
        /// </summary>
        /// <param name="data">The Data that describes the Buff.</param>
        /// <returns>An instance of Buff if found, or null.</returns>
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
        /// Apply a Buff derived from an existing instance. Does not use the passed in instance itself.
        /// </summary>
        /// <param name="buff">An existing instance of a Buff to apply.</param>
        /// <returns>The action that the application was able to perform</returns>
        public Action Apply(Buff buff)
        {
            return this.Apply(buff.data, buff.coolDown);
        }

        /// <summary>
        /// Apply a buff with an optional coolDown.
        /// </summary>
        /// <param name="data">The data to create the Buff from.</param>
        /// <param name="coolDown">An optional coolDown to apply the Buff with.</param>
        /// <returns>The action that the application was able to perform.</returns>
        public Action Apply(Data data, CoolDown.Tracker? coolDown)
        {
            var active = new HashSet<Buff>(this.Active);

            // Refresh Buff if it's already applied and active
            var buff = this.Get(data);
            if (buff != null)
            {
                if (coolDown != null && coolDown.id != buff.coolDown.id)
                    return Action.Failed;

                if (buff.coolDown.Start())
                {
                    if (buff.stacks.Increment())
                        return Action.Incremented;
                    return Action.Refreshed;
                }
                return Action.Failed;
            }

            // Create a new cooldown instance if one wasn't provided.
            coolDown ??= new CoolDown.Tracker(data.duration, new CoolDown.Events(), false);
            buff = new Buff(data, coolDown);

            // Buff can't be applied because it is mutually exclusive or weaker than existing Buff.
            if (active.Where(m => m.data == data || m.data > data).FirstOrDefault() != null)
                return Action.Blocked;

            // Remove weaker Buffs.
            foreach (Buff toRemove in active.Where(m => m.data < data))
                this.Delete(toRemove);

            // Cannot be added as there is no room for active Buffs.
            if (this.Active.Count() >= this.maxMembers)
                return Action.Full;

            // Add Buff and register handler.
            // This should *never* return false.
            // We have already checked for the presence of the Buff using a Data object.
            if (!this.members.Add(buff))
                return Action.Failed;

            if (buff.coolDown.Start())
            {
                buff.stacks.Increment();
                buff.coolDown.subscriber.Subscribe(this.HandleCoolDown);
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

        /// <summary>
        /// Empty the entire set of Buffs.
        /// </summary>
        /// <returns>Whether or not the set was emptied.</returns>
        public bool Empty()
        {
            foreach (Buff toRemove in new HashSet<Buff>(this.members))
                this.Delete(toRemove);

            return this.members.Count == 0;
        }

        /// <summary>
        /// Delete a Buff from the set and cleanup.
        /// </summary>
        /// <param name="buff">The instance of the Buff to process.</param>
        private void Delete(Buff buff)
        {
            buff.stacks.Empty();
            this.members.Remove(buff);
            if (!buff.coolDown.shared)
            {
                buff.coolDown.subscriber.Unsubscribe(this.HandleCoolDown);
                buff.coolDown.Clear();
            }
        }

        /// <summary>
        /// Handler for coolDown messages.
        /// </summary>
        /// <param name="sender">Sender of the message. Unused.</param>
        /// <param name="e">The message to handle.</param>
        private void HandleCoolDown(object? sender, CoolDown.Message e)
        {
            switch (e.Action)
            {
                case CoolDown.Action.Expire:
                    foreach (Buff buff in this.members.Where(m => m.coolDown.id == e.Id))
                    {
                        this.Delete(buff);
                        buff.coolDown.subscriber.Unsubscribe(this.HandleCoolDown);
                    }
                    break;
            }
        }
    }
}
