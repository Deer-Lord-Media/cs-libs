using System;

namespace Quest.Tracker
{
    /// <summary>
    /// Tracks a runtime instance of progress through an <see cref="Data.Event"/>,
    /// recording the furthest stage reached so far.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class, wrapping the given
        /// event data and setting the initial progress.
        /// </summary>
        /// <param name="data">The underlying event data this instance tracks progress for.</param>
        /// <param name="value">
        /// The 1-based stage number to initialize progress to (defaults to <c>0</c>, i.e. no
        /// stage reached yet). See <see cref="Set"/> for how this value is interpreted.
        /// </param>
        public Event(Data.Event data, int value = 1)
        {
            this.data = data;
            Set(value);
        }

        /// <summary>
        /// The underlying event data (stages, id, etc.) this instance tracks progress for.
        /// </summary>
        private readonly Data.Event data;

        /// <summary>
        /// The 0-based index of the furthest stage reached so far.
        /// </summary>
        private int value;

        /// <summary>
        /// Gets the 1-based number of the furthest stage reached so far.
        /// </summary>
        public int Value { get { return value + 1; } }

        /// <summary>
        /// Gets the <see cref="Data.Stage"/> at the current progress index.
        /// </summary>
        public Data.Stage Stage { get { return data.stages[value]; } }

        /// <summary>
        /// Advances progress to the given 1-based stage number, if it is further along
        /// than the current progress, and returns the resulting current stage.
        /// Progress never moves backward.
        /// </summary>
        /// <param name="value">
        /// The 1-based stage number to advance to (i.e. <c>1</c> refers to the first stage).
        /// </param>
        /// <returns>
        /// The <see cref="Data.Stage"/> at the resulting progress index (i.e. the same value
        /// <see cref="Stage"/> would return immediately after this call).
        /// </returns>
        /// <remarks>
        /// The provided <paramref name="value"/> is converted to a 0-based index internally,
        /// compared against the current progress, and only applied if it is greater (via
        /// <see cref="Math.Max(int, int)"/>). The result is then clamped between <c>0</c> and
        /// <c><see cref="Data.Event.count"/> - 1</c> using <see cref="Math.Clamp(int, int, int)"/>,
        /// keeping it within the bounds of <see cref="Data.Event.stages"/>.
        /// </remarks>
        public Data.Stage Set(int value)
        {
            value--;
            var c = Math.Max(value, this.value);
            this.value = Math.Clamp(c, 0, data.count - 1);
            return Stage;
        }
    }
}
