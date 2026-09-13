using System;

namespace Quest.Tracker
{
    public class State
    {
        public State(Data.State data, int value = 0)
        {
            this.data = data;
            Set(value);
        }

        private readonly Data.State data;
        private int value;
        public int Value { get { return value; } }
        public Data.Stage Stage { get { return data.stages[value]; } }

        public void Set(int value)
        {
            value--;
            var c = Math.Max(value, this.value);
            this.value = Math.Clamp(c, 0, data.count);
        }
    }
}
