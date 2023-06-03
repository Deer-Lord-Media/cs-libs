namespace Quests;

public class Container<T> where T : Data
{
    public Container(int maxquests, HashSet<Quest<T>>? members = null)
    {
        this.Maximum = maxquests;
        if (members == null)
        {
            this.members = new HashSet<Quest<T>>();
        } else
        {
            this.members = new HashSet<Quest<T>>(members);
        }
    }

    public int Maximum { get; }
    private HashSet<Quest<T>> members { get; }

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
        if (this.members.Count() < this.Maximum)
        {
            var found = this.members.Where(m => m.Equals(data));
            if (found.Count() == 0)
            {
                Quest<T> quest = new Quest<T>(data);
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
        return this.get(data) != null;
    }

    private Quest<T>? get(T data)
    {
        Quest<T>? quest;
        var members = this.members.Where(m => m.Data.Equals(data));
        quest = (members.Count() > 0) ? members.First() : null;
        return quest;
    }
}
