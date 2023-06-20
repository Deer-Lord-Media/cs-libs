using System;
using System.Collections.Generic;
using System.Linq;

namespace Buffs
{
    public class Tracker
    {
        public Tracker(int maximum, IEnumerable<Buff>? buffs = null)
        {
            this.Maximum = maximum;
            this.members = (buffs == null) ? new HashSet<Buff>() : new HashSet<Buff>(buffs);
        }

        public HashSet<Buff> members;
        public int Maximum { get; }

        public bool IsFull { get { return this.members.Count() >= this.Maximum; } }

        public IEnumerable<Buff> Active
        {
            get
            {
                return this.members.Where(m => !m.IsExpired); // add stack check
            }
        }

        public bool Expire()
        {
            int count = this.members.RemoveWhere(m => m.IsExpired);
            return (count > 0);
        }

        public bool Cancel(Data data)
        {
            int count = this.members.RemoveWhere(m => m.Data == data);
            return (count > 0);
        }

        public bool Apply(Data data)
        {
            bool retval = false;
            IEnumerable<Buff> buffs = this.members.Where(m => m.Data == data);
            if (buffs.Count() > 0)
            // check for this buff
            {
                Buff buff = buffs.First();
                // complicated logic
                // if buff is expired, new application; new buff with 1 stack
                // else if not maxStacks, set stacks to +1
                // else just refresh with maxStacks
                int stacks = (buff.IsExpired)
                    ? 1
                    : Math.Min(data.MaxStacks, buff.Stacks + 1);
                // create new buff; refresh timer with appropriate stack count
                retval = this.members.Remove(buff) && this.members.Add(new Buff(data, stacks));
            } else if (!this.containsBlocker(data) && !this.containsSimilar(data))
            {
                // remove buffs that we override; mandatory
                this.members.RemoveWhere(m => data.HasPrecedenceOver(m.Data));
                if (this.IsFull)
                // check for any buffs we can override and push the first one off
                {
                    buffs = this.members.Where(m => m.Data.PriorityLevel < data.PriorityLevel);
                    if (buffs.Count() > 0) this.members.Remove(buffs.First());
                }
                if (!this.IsFull)
                // if we have room, add it
                {
                    retval = this.members.Add(new Buff(data, 1));
                }
            }
            return retval;
        }

        public bool Remove(Data data)
        {
            bool removed = false;
            IEnumerable<Buff> buffs = this.members.Where(m => m.Data == data);
            if (buffs.Count() > 0)
            {
                Buff buff = buffs.First();
                this.members.Remove(buff);
                if (buff.Stacks > 1)
                {
                    this.members.Add(new Buff(buff.Data, buff.Stacks - 1));
                }
                removed = true;
            }
            return removed;
        }

        public bool Contains(Data data)
        {
            IEnumerable<Buff> buffs = this.members.Where(m => m.Data == data);
            return (buffs.Count() > 0);
        }

        public bool Clear()
        {
            this.members.Clear();
            return this.members.Count() == 0;
        }

        private bool containsSimilar(Data data)
        {
            bool retval = false;
            IEnumerable<Buff> buffs = this.members
                .Where(m => data.IsSimilarTo(m.Data) || m.Data.IsSimilarTo(data));
            retval = (buffs.Count() > 0);
            return retval;
        }

        private bool containsBlocker(Data data)
        {
            IEnumerable<Buff> buffs = this.members.Where(m => m.Data.HasPrecedenceOver(data));
            return (buffs.Count() > 0);
        }
    }
}