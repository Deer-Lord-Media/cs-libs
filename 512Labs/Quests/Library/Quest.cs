namespace Quests;

public struct Quest<T>
{
    public Quest(T data, int currentstage)
    {
        this.Data = data;
        this.CurrentStage = currentstage;
    }
    public Data Data { get; }
    public int CurrentStage { get; }

    public bool Complete
    {
        get
        {
            return this.CurrentStage > this.Data.TotalStages;
        }
    }

    public Quest<T>? Progress()
    {
        Quest<T>? quest = (this.Complete) ? null : new Quest<T>(this.Data, this.CurrentStage + 1);
        return quest;
    }
}
