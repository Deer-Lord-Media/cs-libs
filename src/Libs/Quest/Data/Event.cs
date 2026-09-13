using System;

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
            this.stages = stages;
            count = stages.Length;
        }
    }
}
