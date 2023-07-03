using System;

namespace CSLibs.Items
{
    public class Item
    {
        public Item(Data data, int? charges = null, CoolDown? coolDown = null)
        {
            this.Data = data;
            this.charges = (charges == null)
                ? this.Data.MaxCharges
                : (int)charges;
            this.CoolDown = (coolDown == null)
                ? new CoolDown(data.CoolDownDuration)
                : (CoolDown)coolDown;
        }
        public Data Data { get; }
        public CoolDown CoolDown { get; }
        private int charges;
        public int Charges { get { return this.charges; } set { this.charges =  Math.Max(0, value); } }

        public bool Use()
        {
            bool retval = false;

            if (this.IsUsable && this.CoolDown.Start())
            {
                if (this.Data.MaxCharges > 0)
                {
                    this.Charges = this.Charges - 1;
                }
                retval = true;
            }
            return retval;
        }

        public bool IsUsable
        {
            get
            {
                bool chargesAvailable = this.Data.MaxCharges == 0 || this.Charges > 0;
                return (chargesAvailable && this.CoolDown.Ready);
            }
        }
    }
}