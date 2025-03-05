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

        public bool Add(Buff buff)
        {
            HashSet<Buff> active = (HashSet<Buff>)this.Active;
            var exact = active.Where(m => m.data.Id == buff.data.Id).FirstOrDefault();
            if (exact != null)
            {
                return exact.coolDown.Refresh();
            }
            var excluded = active.Where(m => buff == m || buff < m).FirstOrDefault();
            if (excluded != null)
            {
                return false;
            }
            if (active.Count == this.maxMembers)
            {
                var pushedOff = active.Where(m => buff > m).FirstOrDefault();
                if (pushedOff != null)
                {
                    var cleared = pushedOff.coolDown.Clear();
                    if (!cleared)
                    {
                        return false;
                    }
                    this.members.Remove(pushedOff);
                }
            }
            return this.members.Add(buff);
        }
    }
}
