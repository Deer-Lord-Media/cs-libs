using System;
using System.Collections.Generic;
using System.Linq;

namespace CSLibs.Buffs
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

        private List<Buff> cache = new List<Buff>();

        public List<Buff> Active
        {
            get
            {
                IEnumerable<Buff> members = this.members.Where(m => !m.IsExpired && m.Stacks > 0);
                return new List<Buff>(members);
            }
        }

        public bool Expire()
        {
            int count;

            count = this.members.RemoveWhere(m => m.IsExpired);
            return count > 0;
        }

        public Buff? Cancel(Data data)
        {
            Buff? buff = null;
            IEnumerable<Buff> members;

            members = this.members.Where(m => m.Data == data);
            if (members.Count() > 0) buff = members.First();
            return buff;
        }

        public Buff? Apply(Data data)
        {
            Buff buff;
            IEnumerable<Buff> buffs;
            Buff? retval = null;
            
            buffs = this.members.Where(m => m.Data == data);
            if (buffs.Count() > 0)
            // check for this buff
            {
                buff = buffs.First();
                // complicated logic
                // if buff is expired, new application; new buff with 1 stack
                // else if not maxStacks, set stacks to +1
                // else just refresh with maxStacks
                int stacks = (buff.IsExpired)
                    ? 1
                    : Math.Min(data.MaxStacks, buff.Stacks + 1);
                bool removed = this.members.Remove(buff);
                buff = new Buff(data, stacks);
                bool added = this.members.Add(buff);
                if (added)
                {
                    retval = buff;
                }
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
                    buff = new Buff(data, 1);
                    if (this.members.Add(buff))
                    {
                        retval = buff;
                    }
                }
            }
            return retval;
        }

        public Buff? Remove(Data data)
        {
            Buff? retval = null;
            IEnumerable<Buff> buffs = this.members.Where(m => m.Data == data);
            if (buffs.Count() > 0)
            {
                Buff buff = buffs.First();
                this.members.Remove(buff);
                if (buff.Stacks > 1)
                {
                    retval = new Buff(buff.Data, buff.Stacks - 1, buff.At);
                    this.members.Add(buff);
                }
            }
            return retval;
        }

        public bool Contains(Data data)
        {
            IEnumerable<Buff> buffs;
            
            buffs = this.members.Where(m => m.Data == data);
            return (buffs.Count() > 0);
        }

        public bool Clear()
        {
            bool retval;

            this.members.Clear();
            retval = this.members.Count() == 0;
            return retval;
        }

        private bool containsSimilar(Data data)
        {
            IEnumerable<Buff> buffs;
            bool retval = false;
            
            buffs = this.members
                .Where(m => data.IsSimilarTo(m.Data) || m.Data.IsSimilarTo(data));
            retval = (buffs.Count() > 0);
            return retval;
        }

        private bool containsBlocker(Data data)
        {
            IEnumerable<Buff> buffs;
            
            buffs = this.members.Where(m => m.Data.HasPrecedenceOver(data));
            return (buffs.Count() > 0);
        }
    }
}
