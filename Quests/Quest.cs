using System;

namespace CSLibs.Quests
{
    public class Quest
    {
        public Quest(Data data, int currentStage = 0, bool tracked = false, bool turnedIn = false)
        {
            this.Data = data;
            this.CurrentStage = Math.Clamp(currentStage, 0, data.TotalStages);
            this.Tracked = tracked;
            this.TurnedIn = turnedIn;
        }
        public Data Data { get; }
        public int CurrentStage { get; }
        public bool IsComplete { get { return this.CurrentStage >= this.Data.TotalStages; } }
        public bool Tracked { get; }
        public bool TurnedIn { get; }
    }
}
