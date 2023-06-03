using System.Collections.Generic;
using System.Linq;
namespace Quests;

public class Container<T> where T : Data
{
    public Container(int maxquests, HashSet<Quest<T>>? members = null)
    {
        this.MaxQuests = maxquests;
        this.members = (members == null) ? new HashSet<Quest<T>>() : new HashSet<Quest<T>>(members);
    }

    public int MaxQuests { get; }
    public HashSet<Quest<T>> members { get; }
    
    public HashSet<Quest<T>> All
    {
        get
        {
            return new HashSet<Quest<T>>(this.members);
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

   public bool? Remove(T data)
    {
        bool? retval = null;
        if(this.Contains(data))
        {
            int count = this.members.RemoveWhere(m => m.Data.Equals(data));
            retval = count != 0;
        }
        return retval;
    }

    public bool Contains(T data)
    {
        var members = this.members.Where(m => m.Data.Equals(data));
        return (members.Count() != 0);
    }
}
