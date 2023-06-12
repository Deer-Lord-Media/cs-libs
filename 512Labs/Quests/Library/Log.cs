namespace Quests;

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
        if (!this.IsFull && !this.Contains(data))
        {
           this.members.Add(new Quest(data));
        }
        return this.Contains(data);
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

    public void Advance(Data data)
    {
        Quest? quest = this.Get(data);
        if (quest != null)
        {
            this.members.Remove(quest);
            this.members.Add(new Quest(data, quest.CurrentStage + 1));
        }
    }

    private Quest? Get(Data data)
    {
        Quest? quest;
        IEnumerable<Quest> members = this.members.Where(m => m.Data == data);
        quest = (members.Count() > 0) ? members.First() : null;
        return quest;
    }
}
