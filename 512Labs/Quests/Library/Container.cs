namespace Quests;

public class Container<T> where T : Data
{
    public Container(int maximum, IEnumerable<Quest<T>>? members = null)
    {
        this.Maximum = maximum;
        this.members = (members == null) ? new HashSet<Quest<T>>() : new HashSet<Quest<T>>(members);
        this.tracked = new HashSet<Quest<T>>();
    }

    public int Maximum { get; }
    private HashSet<Quest<T>> members { get; }
    private HashSet<Quest<T>> tracked;
    public HashSet<Quest<T>> Tracked { get { return new HashSet<Quest<T>>(this.tracked); } }

    public bool Full { get { return this.members.Count() == this.Maximum; } }

    public HashSet<Quest<T>> All { get { return new HashSet<Quest<T>>(this.members); } }

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
        bool removed;
        int count = this.members.RemoveWhere(m => m.Data == data);
        removed = count > 0;
        return removed;
    }

    public bool Contains(T data)
    {
        return this.get(data) != null;
    }

    public bool Track(T data)
    {
        bool tracked = false;
        Quest<T>? quest = this.get(data);
        if (quest != null)
        {
            this.tracked.Add(quest);
            tracked = true;
        }
        return tracked;
    }

    public bool Untrack(T data)
    {
        bool tracked = false;
        Quest<T>? quest = this.get(data);
        if (quest != null)
        {
            this.tracked.Remove(quest);
            tracked = true;
        }
        return tracked;
    }

    private Quest<T>? get(T data)
    {
        Quest<T>? quest;
        var members = this.members.Where(m => m.Data == data);
        quest = (members.Count() > 0) ? members.First() : null;
        return quest;
    }
}
