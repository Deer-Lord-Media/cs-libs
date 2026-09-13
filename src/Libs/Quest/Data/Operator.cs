using System;

namespace Quest.Data
{
    /// <summary>
    /// Provides comparison operator functions that can be resolved dynamically
    /// from a <see cref="Symbol"/> value. Used by <see cref="Comparator"/> to
    /// evaluate integer comparisons based on a stored operator symbol.
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// Resolves the comparison function associated with the given <see cref="Symbol"/>.
        /// </summary>
        /// <param name="symbol">The symbol identifying which comparison operator to use.</param>
        /// <returns>
        /// A <see cref="Func{T1, T2, TResult}"/> that takes a reference value and an
        /// input value (in that order) and returns whether the comparison holds;
        /// or <c>null</c> if <paramref name="symbol"/> does not match a known operator.
        /// </returns>
        public static Func<int, int, bool>? Function(Symbol symbol)
        {
            Func<int, int, bool>? op = null;
            switch (symbol)
            {
                case Symbol.GREATER_THAN:
                    op = GreaterThan;
                    break;
                case Symbol.LESS_THAN:
                    op = LessThan;
                    break;
                case Symbol.AT_LEAST:
                    op = AtLeast;
                    break;
                case Symbol.OR_LESS:
                    op = OrLess;
                    break;
                case Symbol.EQUAL:
                    op = Equal;
                    break;
            }
            return op;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is strictly greater than <paramref name="gt"/>.
        /// </summary>
        /// <param name="gt">The threshold value.</param>
        /// <param name="o">The value being tested.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &gt; <paramref name="gt"/>; otherwise <c>false</c>.</returns>
        private static bool GreaterThan(int gt, int o)
        {
            return o > gt;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is strictly less than <paramref name="lt"/>.
        /// </summary>
        /// <param name="lt">The threshold value.</param>
        /// <param name="o">The value being tested.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &lt; <paramref name="lt"/>; otherwise <c>false</c>.</returns>
        private static bool LessThan(int lt, int o)
        {
            return o < lt;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is greater than or equal to <paramref name="al"/>.
        /// </summary>
        /// <param name="al">The minimum threshold value.</param>
        /// <param name="o">The value being tested.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &gt;= <paramref name="al"/>; otherwise <c>false</c>.</returns>
        private static bool AtLeast(int al, int o)
        {
            return o >= al;
        }

        /// <summary>
        /// Determines whether <paramref name="o"/> is less than or equal to <paramref name="ol"/>.
        /// </summary>
        /// <param name="ol">The maximum threshold value.</param>
        /// <param name="o">The value being tested.</param>
        /// <returns><c>true</c> if <paramref name="o"/> &lt;= <paramref name="ol"/>; otherwise <c>false</c>.</returns>
        private static bool OrLess(int ol, int o)
        {
            return o <= ol;
        }

        /// <summary>
        /// Determines whether <paramref name="eq"/> and <paramref name="o"/> are equal.
        /// </summary>
        /// <param name="eq">The reference value.</param>
        /// <param name="o">The value being tested.</param>
        /// <returns><c>true</c> if the two values are equal; otherwise <c>false</c>.</returns>
        private static bool Equal(int eq, int o)
        {
            return eq == o;
        }
    }
}
