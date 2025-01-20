using System;

namespace CSLibs.Items
{
    public class Item
    {
        public Item(Data data, int? charges = null, CoolDown.Tracker? coolDown = null)
        {
            this.Data = data;
            this.charges = (charges == null)
                ? Data.MaxCharges == null ? 0 : (int)Data.MaxCharges // 0 if null
                : (int)charges;
            this.CoolDown = coolDown ?? new CoolDown.Tracker(data.CoolDownDuration);
        }
        public Data Data { get; }
        public CoolDown.Tracker CoolDown { get; }
        private int charges;
        public int Charges
        {
            get { return this.charges; }
            set
            {
                this.charges = Math.Clamp(
                    value,
                    0,
                    Data.MaxCharges == null ? 0 : (int)Data.MaxCharges
                );
            }
        }

        public bool Use()
        {
            bool used = this.IsUsable && this.CoolDown.Start();
            if (used && this.Data.MaxCharges > 0)
            {
                this.Charges--;
            }
            return used;
        }

        public bool IsUsable
        {
            get
            {
                return (
                    // has a cooldown
                    (this.CoolDown.Duration != TimeSpan.Zero)
                    // if it has charges, make sure we have some left
                    && (this.Data.MaxCharges == null || this.Charges > 0)
                    // make sure we're off cooldown
                    && this.CoolDown.Ready
                );
            }
        }
    }
}
