using System;

namespace Quests
{
    public class Quest
    {
        public Quest(Data data, int currentStage = 0, bool tracked = false)
        {
            this.Data = data;
            this.CurrentStage = Math.Max(0, Math.Min(currentStage, data.TotalStages));
            this.Tracked = tracked;
        }
        public Data Data { get; }
        public int CurrentStage { get; }
        public bool IsComplete { get { return this.CurrentStage >= this.Data.TotalStages; } }
        public bool Tracked { get; }
    }
}