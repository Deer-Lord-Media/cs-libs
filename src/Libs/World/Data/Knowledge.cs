using System;

namespace World.Data
{
    /// <summary>
    /// Represents a fact that one person knows about another person's knowledge
    /// of a detail belonging to a specific event.
    /// </summary>
    /// <remarks>
    /// Conceptually reads as: "<see cref="knowerId"/> knows that <see cref="knowsId"/>
    /// knows a detail (<see cref="value"/>) about <see cref="eventId"/>."
    /// This allows modeling layered or second-hand knowledge (e.g. rumors, gossip,
    /// or awareness of what others have witnessed) rather than just direct knowledge
    /// of an event.
    /// </remarks>
    public readonly struct Knowledge
    {
        /// <summary>
        /// The identifier of the person who holds this piece of knowledge.
        /// </summary>
        public readonly Guid knowerId;

        /// <summary>
        /// The identifier of the person who is known (by <see cref="knowerId"/>)
        /// to know something about the event.
        /// </summary>
        public readonly Guid knowsId;

        /// <summary>
        /// A value representing the specific detail known about the event.
        /// The meaning of this value is context-dependent (e.g. a stage index,
        /// a detail type, or an enum cast to <see cref="int"/>).
        /// </summary>
        public readonly Assurance assurance;
        // these could potentially be a specific set of flags

        /// <summary>
        /// The identifier of the event this knowledge pertains to.
        /// </summary>
        public readonly Guid eventId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Knowledge"/> struct.
        /// </summary>
        /// <param name="knowerId">The person who holds this piece of knowledge.</param>
        /// <param name="knowsId">The person who is known to know something about the event.</param>
        /// <param name="assurance">The specific detail known about the event.</param>
        /// <param name="eventId">The event this knowledge pertains to.</param>
        public Knowledge(Guid knowerId, Guid knowsId, Assurance assurance, Guid eventId)
        {
            this.knowerId = knowerId;
            this.knowsId = knowsId;
            this.assurance = assurance;
            this.eventId = eventId;
        }
    }
}
