using System;
using System.Linq;

namespace World.Data
{
    /// <summary>
    /// Represents an encounter: a named, described interaction composed of an
    /// ordered sequence of possible states.
    /// </summary>
    public readonly struct Encounter
    {
        /// <summary>
        /// The unique identifier for this encounter.
        /// </summary>
        public readonly Guid id;

        /// <summary>
        /// The display name of this encounter.
        /// </summary>
        public readonly string name;

        /// <summary>
        /// A human-readable description of this encounter.
        /// </summary>
        public readonly string description;

        /// <summary>
        /// The set of states associated with this encounter.
        /// </summary>
        public readonly State[] states;

        /// <summary>
        /// Initializes a new instance of the <see cref="Encounter"/> struct.
        /// </summary>
        /// <param name="id">The unique identifier for this encounter.</param>
        /// <param name="name">The display name of this encounter.</param>
        /// <param name="description">A human-readable description of this encounter.</param>
        /// <param name="states">The set of states associated with this encounter.</param>
        public Encounter(Guid id, string name, string description, State[] states)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.states = states;
        }
    }
}