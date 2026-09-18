using System;

namespace World.Data
{
    /// <summary>
    /// Represents a single stage within an <see cref="Event"/>'s sequence of stages.
    /// </summary>
    public readonly struct Stage
    {
        /// <summary>
        /// The identifier of the <see cref="Event"/> this stage belongs to.
        /// </summary>
        public readonly Guid eventId;

        /// <summary>
        /// The 1-based position of this stage within its event's sequence.
        /// </summary>
        public readonly int number;

        /// <summary>
        /// A human-readable description of this stage.
        /// </summary>
        public readonly string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stage"/> struct.
        /// </summary>
        /// <param name="eventId">The identifier of the <see cref="Event"/> this stage belongs to.</param>
        /// <param name="number">The 1-based position of this stage within its event's sequence.</param>
        /// <param name="description">A human-readable description of this stage.</param>
        public Stage(Guid eventId, int number, string description)
        {
            this.eventId = eventId;
            this.number = number;
            this.description = description;
        }
    }
}
