using System.Collections.Generic;
using System.Linq;

namespace CSLibs.Quests
{
    public class Log
    {
        public Log(int maximum)
        {
            this.Maximum = maximum;
            this.members = new HashSet<Quest>();
        }

        public int Maximum { get; }
        private HashSet<Quest> members { get; }
        public HashSet<Quest> All { get { return new HashSet<Quest>(this.members); } }
        public bool IsFull { get { return this.members.Count() >= this.Maximum; } }

        public bool Add(Data data)
        {
            bool retval = false;
            var members = this.members.Where(m => m.Data == data);
            if (!this.IsFull && !this.Contains(data))
            {
            this.members.Add(new Quest(data));
            retval = true;
            }
            return retval;
        }

    public bool Remove(Data data)
        {
            bool removed;
            int count = this.members.RemoveWhere(m => m.Data == data);
            removed = count > 0;
            return removed;
        }

        public bool Contains(Data data)
        {
            return this.Get(data) != null;
        }

        public bool Track(Data data)
        {
            bool retval = false;
            Quest? quest = this.Get(data);
            if (quest != null)
            {
                this.members.Remove(quest);
                this.members.Add(new Quest(data, quest.CurrentStage, true));
            }
            return retval;
        }

        public bool Untrack(Data data)
        {
            bool retval = false;
            Quest? quest = this.Get(data);
            if (quest != null)
            {
                this.members.Remove(quest);
                this.members.Add(new Quest(data, quest.CurrentStage, false));
            }
            return retval;
        }

        public bool Advance(Data data)
        {
            bool retval = false;
            var members = this.members.Where(m => m.Data == data);
            if (members.Count() > 0)
            {
                var quest = members.First();
                this.members.Remove(quest);
                retval = this.members.Add(new Quest(data, quest.CurrentStage + 1));
            }
            return retval;
        }

        private Quest? Get(Data data)
        {
            Quest? quest;
            IEnumerable<Quest> members = this.members.Where(m => m.Data == data);
            quest = (members.Count() > 0) ? members.First() : null;
            return quest;
        }
    }
}