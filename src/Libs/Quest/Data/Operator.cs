using System;

namespace Quest.Data
{
    public class Operator
    {
        public static Func<int, int, bool>? Function(string symbol)
        {
            Func<int, int, bool>? op = null;
            switch (symbol)
            {
                case ">":
                    op = GreaterThan;
                    break;
                case "<":
                    op = LessThan;
                    break;
                case ">=":
                    op = AtLeast;
                    break;
                case "<=":
                    op = OrLess;
                    break;
                case "==":
                    op = Equal;
                    break;
            }
            return op;
        }

        private static bool GreaterThan(int gt, int o)
        {
            return o > gt;
        }

        private static bool LessThan(int lt, int o)
        {
            return o < lt;
        }

        private static bool AtLeast(int al, int o)
        {
            return o >= al;
        }

        private static bool OrLess(int ol, int o)
        {
            return o <= ol;
        }

        private static bool Equal(int eq, int o)
        {
            return eq == o;
        }
    }
}