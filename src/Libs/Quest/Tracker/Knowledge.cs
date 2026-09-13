namespace Quest.Tracker
{
    public class Knowledge
    {

        private readonly Data.Knowledge data;
        private int flags;

        public int Flags
        {
            get
            {
                return flags;
            }
            set
            {
                flags |= value;
            }
        }

        public Knowledge(Data.Knowledge data)
        {
            this.data = data;
            flags = this.data.flags;
        }
    }
}
