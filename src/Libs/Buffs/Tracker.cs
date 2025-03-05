using System;
using System.Collections.Generic;
using System.Linq;
using CoolDown;

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

        public HashSet<Buff> Active
        {
            get
            {
                return (HashSet<Buff>)this.members.Where(m => m.coolDown.TimeRemaining > TimeSpan.Zero);
            }
        }

        public bool Add(Buff buff)
        {
            var exact = this.members.Where(m => m.data.Id == buff.data.Id).FirstOrDefault();
            if (exact != null)
            {
                return exact.coolDown.Refresh();
            }
            var excluded = this.members.Where(m => buff == m || buff < m).FirstOrDefault();
            if (excluded != null)
            {
                return false;
            }
            if (this.Active.Count == this.maxMembers)
            {
                var pushedOff = this.members.Where(m => buff > m).FirstOrDefault();
                if (pushedOff != null)
                {
                    var cleared = pushedOff.coolDown.Clear();
                    if (!cleared)
                    {
                        return false;
                    }
                }
            }
            return this.members.Add(buff);
        }
    }
}
