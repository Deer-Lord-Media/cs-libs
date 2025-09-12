using System;

namespace Resource
{
    public class Tracker
    {
        public Tracker(int max, int min)
        {
            this.min = min;
            this.max = max;
        }

        private int min;
        private int max;
        private int value;
        public int Value
        {
            get { return this.value; }
        }

        public (int, int) Range()
        {
            return (this.min, this.max);
        }

        /// <summary>
        /// Increase the value by 1, if possible.
        /// </summary>
        /// <returns>Success of increasing the value.</returns>
        public bool Increment()
        {
            if (this.value < this.max)
            {
                this.value++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Decreases the value by 1, if possible.
        /// </summary>
        /// <returns>Success of decreasing the value.</returns>
        public bool Decrement()
        {
            if (this.value > this.min)
            {
                this.value--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add a specific value to the current value without overflowing the maximum.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The amount added.</returns>
        public int Add(int value)
        {
            var emptySize = this.max - this.value;
            value = Math.Min(value, emptySize);
            this.value = Math.Min(this.value + value, this.max);
            return value;
        }

        /// <summary>
        /// Subtract a specific value to the current value without underflowing the minimum.
        /// </summary>
        /// <param name="value">The value to subract.</param>
        /// <returns>The amount subtracted.</returns>
        public int Sub(int value)
        {
            var remainder = this.value - this.min;
            value = Math.Min(value, remainder);
            this.value = Math.Max(this.value - value, this.min);
            return value;
        }

        /// <summary>
        /// Fill the resource to the maximum.
        /// </summary>
        public void Fill()
        {
            this.value = this.max;
        }

        /// <summary>
        /// Empty the resource to the minimum.
        /// </summary>
        public void Empty()
        {
            this.value = this.min;
        }

        /// <summary>
        /// Zero the resource out, or as close as possible.
        /// </summary>
        public void Zero()
        {
            this.value = Math.Min(Math.Max(0, this.min), this.max);
        }

        /// <summary>
        /// Get the ratio of the current value to the total range.
        /// </summary>
        public int Ratio
        {
            get
            {
                float value = (float)this.value / (float)(this.max - this.min);
                return (int)(value * 100f); // ((float)this.Value / denominator) * factor;
            }
        }

        /// <summary>
        /// Change the maximum value of the tracker.
        /// </summary>
        /// <param name="maxChange">Amount to change the maximum by.</param>
        /// <param name="minChange">Amount to change the minimum by.</param>
        public void Change(int? maxChange, int? minChange)
        {
            if (minChange != null) this.min += (int)minChange;
            if (maxChange != null) this.max += (int)maxChange;
        }
    }
}
