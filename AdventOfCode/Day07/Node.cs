using System.Collections.Generic;

namespace AdventOfCode.Day07
{
    public class Node
    {
        public string Label { get; }
        public HashSet<string> Prev { get; } = new HashSet<string>();
        public HashSet<string> Next { get; } = new HashSet<string>();

        public Node(string label, IEnumerable<Node> prev = null, IEnumerable<Node> next = null)
        {
            Label = label;

            foreach (var node in prev ?? new HashSet<Node>())
            {
                Prev.Add(node.Label);
            }

            foreach (var node in next ?? new HashSet<Node>())
            {
                Next.Add(node.Label);
            }
        }

        public override int GetHashCode()
        {
            return Label.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is Node)) return false;
            return ((Node)other).Label == Label;
        }
    }
}
