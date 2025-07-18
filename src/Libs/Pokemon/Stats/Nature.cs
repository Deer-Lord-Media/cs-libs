namespace Pokemon.Stats
{
    public class Nature
    {
        public Nature(string name, Name boost, Name degrade)
        {
            this.name = name;
            this.boost = boost;
            this.degrade = degrade;
        }
        public readonly string name;
        public readonly Name boost;
        public readonly Name degrade;
    }
}
