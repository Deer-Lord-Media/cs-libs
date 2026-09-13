using System;

namespace Quest.Data
{
    /// <summary>
    /// Represents a comparison rule that evaluates an integer value against
    /// a stored threshold using a specified operator (e.g. "==", ">", "&lt;=")
    /// relative to another entity or context
    /// (identified by <see cref="againstId"/> and <see cref="againstType"/>).
    /// </summary>
    public readonly struct Comparator
    {
        /// <summary>
        /// The identifier of the Encounter this comparator belongs to.
        /// </summary>
        public readonly Guid encounterId;

        /// <summary>
        /// The unique identifier of the entity being compared against.
        /// </summary>
        public readonly Guid againstId;

        /// <summary>
        /// Specifies the type/category of the entity referenced by <see cref="againstId"/>.
        /// </summary>
        public readonly Against againstType;

        /// <summary>
        /// The comparison operator symbol (e.g. "==", "!=", "&gt;", "&lt;", "&gt;=", "&lt;=")
        /// used to determine how <see cref="value"/> is compared against an input value.
        /// Strings do not work, yet, only integer/enum
        /// </summary>
        public readonly Symbol symbol;

        /// <summary>
        /// The reference value that an input is compared against, using the
        /// operator defined by <see cref="symbol"/>.
        /// </summary>
        public readonly int value;

        /// <summary>
        /// Evaluates whether <paramref name="other"/> satisfies this comparator's
        /// condition, by applying the operator function associated with
        /// <see cref="symbol"/> to <see cref="value"/> and <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The value to compare against <see cref="value"/>.</param>
        /// <returns>
        /// <c>true</c> if a valid operator function exists for <see cref="symbol"/>
        /// and the comparison succeeds; otherwise <c>false</c>.
        /// </returns>
        public bool Compare(int other)
        {
            var func = Operator.Function(symbol);
            return func != null && func(value, other);
        }
    }
}
