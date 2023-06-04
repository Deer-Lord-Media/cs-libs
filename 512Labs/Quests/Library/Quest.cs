namespace Quests;

public class Quest<T> where T : Data
{
    public Quest(T data, int currentstage = 0)
    {
        this.Data = data;
        this.currentStage = currentstage;
    }
    public Data Data { get; }
    private int currentStage;
    public int CurrentStage { get { return this.currentStage; } }
    public bool IsComplete { get { return this.CurrentStage > this.Data.TotalStages; } }

    public bool Advance()
    {
        bool retval = false;
        if (this.currentStage <= this.Data.TotalStages)
        {
            this.currentStage++;
            retval = true;
        }
        return retval;
    }
}
