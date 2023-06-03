using System.Collections.Generic;
using System.Linq;
namespace Quests;

public class Container<T> where T : Data
{
    public Container(int maxquests, HashSet<Quest<T>>? members = null)
    {
        this.MaxQuests = maxquests;
        this.members = new HashSet<T>(members);
    }

    public int MaxQuests { get; }
    private HashSet<Quest<T>> members { get; }
    public HashSet<Quest<T>> All
    {
        get
        {
            return new HashSet<string>(this.members);
        }
    }

    public bool Add(T data)
    {
        bool retval = false;
        if (this.members.Count() < this.MaxQuests)
        {
            var found = this.members.Where(m => m.Equals(data));
            if (found.Count() == 0)
            {
                Quest<T> quest = new Quest<T>(data, 0);
                this.members.Add(quest);
                retval = true;
            }
        }
        return retval;
    }

    public bool Progress(T data)
    {
        bool retval = false;
        if (this.Has(data))
        {
            var found = this.members.Where(m => m.Equals(data));
            var member = found.First();
            Quest<T> quest = member.Progress();
            this.members.RemoveWhere(m => m.Equals(data));
            this.members.Add(quest);
            retval = true;
        }
        return retval;
    }

   public bool? Remove(T data)
    {
        bool? retval = null;
        if(this.Has(data))
        {
            int count = this.members.RemoveWhere(m => m.Data.Equals(data));
            retval = count != 0;
        }
        return retval;
    }

    public bool Has(T data)
    {
        var trackers = this.members.Where(m => m.Data.Equals(data));
        return (trackers.Count() != 0);
    }
}
