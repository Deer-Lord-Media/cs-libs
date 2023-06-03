namespace Quests;

public class Container<T> where T : Data
{
    public Container(int maximum, IEnumerable<Quest<T>>? members = null)
    {
        this.Maximum = maximum;
        this.members = (members == null) ? new HashSet<Quest<T>>() : new HashSet<Quest<T>>(members);
    }

    public int Maximum { get; }
    private HashSet<Quest<T>> members { get; }

    public bool Full
    {
        get
        {
            return this.members.Count() == this.Maximum;
        }
    }

    public HashSet<Quest<T>> All
    {
        get
        {
            return new HashSet<Quest<T>>(this.members);
        }
    }

    public bool Add(T data)
    {
        if (!this.Contains(data) && !this.Full)
        {
           this.members.Add(new Quest<T>(data));
        }
        return this.Contains(data);
    }

   public bool Remove(T data)
    {
        bool removed = false;
        if(this.Contains(data))
        {
            int count = this.members.RemoveWhere(m => m.Data == data);
            removed = count > 0;
        }
        return removed;
    }

    public bool Contains(T data)
    {
        return this.get(data) != null;
    }

    private Quest<T>? get(T data)
    {
        Quest<T>? quest;
        var members = this.members.Where(m => m.Data == data);
        quest = (members.Count() > 0) ? members.First() : null;
        return quest;
    }
}
