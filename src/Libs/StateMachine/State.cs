using System;
using System.Collections.Generic;
using System.Linq;

namespace StateMachine
{
    public class State : IEquatable<State>
    {
        public State(string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            children = new HashSet<State>();
        }

        public readonly string name;
        public readonly HashSet<State> children;

        public void Add(State child)
        {
            children.Add(child);
        }

        public bool Equals(State other) =>
            other is object &&
            string.Equals(name, other.name, StringComparison.Ordinal);

        public override bool Equals(object obj) => Equals(obj as State);

        public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(name);

        public static bool operator ==(State a, State b) =>
            a is null ? b is null : a.Equals(b);

        public static bool operator !=(State a, State b) => !(a == b);
    }
    public class Validator
    {
        public static bool Validate(State start, string name)
        {
            // Forward pass: collect every state reachable from start.
            // Nodes named `name` aren't expanded further (the path has "looped back"),
            // except `start` itself, which must be expanded to find its way back.
            var reverse = new Dictionary<string, List<string>>(); // child -> parents
            var reachable = new HashSet<string>();
            var hitsTarget = new Queue<string>();                 // nodes with a direct edge to `name`
            var stack = new Stack<State>();

            reachable.Add(start.name);
            stack.Push(start);

            while (stack.Count > 0)
            {
                State current = stack.Pop();
                bool direct = false;

                if (current.children != null)
                {
                    foreach (State child in current.children)
                    {
                        if (child.name == name)
                        {
                            direct = true;      // this edge reaches the target
                            continue;           // don't expand past it
                        }

                        if (!reverse.TryGetValue(child.name, out var parents))
                            reverse[child.name] = parents = new List<string>();
                        parents.Add(current.name);

                        if (reachable.Add(child.name))
                            stack.Push(child);
                    }
                }

                if (direct) hitsTarget.Enqueue(current.name);
            }

            // Backward pass: everything that can reach a node with a direct edge to `name`.
            var good = new HashSet<string>(hitsTarget);
            while (hitsTarget.Count > 0)
            {
                string node = hitsTarget.Dequeue();
                if (!reverse.TryGetValue(node, out var parents)) continue;

                foreach (string p in parents)
                    if (good.Add(p))
                        hitsTarget.Enqueue(p);
            }

            // Valid only if every reachable state can get back to `name`.
            return reachable.All(good.Contains);
        }
    }
}
