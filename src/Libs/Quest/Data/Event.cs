using System;
using System.Linq;

namespace Quest.Data
{
    /// <summary>
    /// Represents an event composed of an ordered sequence of stages.
    /// These are things that are happening around the world, and
    /// may not pertain to a particular person or entity.
    /// 
    /// It may be possible for an event to be occurring and an entity
    /// not yet know about it.
    /// </summary>
    public readonly struct Event
    {
        /// <summary>
        /// The unique identifier for this event.
        /// </summary>
        public readonly Guid id;

        /// <summary>
        /// The ordered array of stages that make up this event.
        /// </summary>
        public readonly Stage[] stages;

        /// <summary>
        /// The total number of stages in this event.
        /// Cached from <see cref="stages"/>.Length at construction time.
        /// </summary>
        public readonly int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> struct.
        /// </summary>
        /// <param name="id">The unique identifier for this event.</param>
        /// <param name="stages">The ordered array of stages that make up this event.</param>
        public Event(Guid id, Stage[] stages)
        {
            this.id = id;
            this.stages = stages.OrderBy(s => s.number).ToArray();
            count = stages.Length;
        }

        /// <summary>
        /// Retrieves the <see cref="Stage"/> with the given 1-based stage number.
        /// </summary>
        /// <param name="number">
        /// The 1-based number of the stage to retrieve (i.e. the first stage is <c>1</c>, not <c>0</c>).
        /// </param>
        /// <returns>
        /// The matching <see cref="Stage"/> if <paramref name="number"/> corresponds to the stage
        /// at that position in <see cref="stages"/>; otherwise <c>null</c> if the stage stored at
        /// that position does not have a matching <see cref="Stage.number"/>.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if <paramref name="number"/> is less than 1 or greater than the number of stages.
        /// </exception>
        public Stage? Stage(int number)
        {
            number = Math.Clamp(number, 1, stages.Count());
            var stage = stages[number - 1];
            if (stage.number != number)
            {
                return null;
            }

            return stage;
        }
    }
}
