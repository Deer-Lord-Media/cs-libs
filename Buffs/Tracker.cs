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
            this.lastCached = DateTime.Now;
            this.lastUpdated = DateTime.Now;
        }

        public HashSet<Buff> members;
        public int Maximum { get; }
        public bool IsFull { get { return this.members.Count() >= this.Maximum; } }

        private DateTime lastUpdated;
        private DateTime lastCached;
        private List<Buff> cache = new List<Buff>();

        public List<Buff> Active
        {
            get
            {
                if (this.lastUpdated >= this.lastCached)
                {
                    IEnumerable<Buff> members = this.members.Where(m => !m.IsExpired && m.Stacks > 0);
                    this.cache.Clear();
                    this.cache.AddRange(members);
                    this.lastCached = DateTime.Now;
                }
                return this.cache;
            }
        }

        public bool Expire()
        {
            int count;
            bool retval;

            count = this.members.RemoveWhere(m => m.IsExpired);
            if (count > 0)
            {
                this.lastUpdated = DateTime.Now;
                retval = true;
            } else
            {
                retval = false;
            }
            return retval;
        }

        public Buff? Cancel(Data data)
        {
            Buff? buff = null;
            IEnumerable<Buff> members;

            members = this.members.Where(m => m.Data == data);
            if (members.Count() > 0)
            {
                buff = members.First();
                this.lastUpdated = DateTime.Now;
            }
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
            if (retval != null)
            {
                this.lastUpdated = DateTime.Now;
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
                    this.lastUpdated = DateTime.Now;
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
            bool retval = false;

            this.members.Clear();
            if (this.members.Count() == 0)
            {
                retval = true;
                this.lastUpdated = DateTime.Now;
            }
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
