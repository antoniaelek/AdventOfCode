using System;

namespace AdventOfCode.Day07
{
    public class Node : IComparable
    {
        public char Label { get; }

        public Node(char label)
        {
            Label = label;
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

        public int CompareTo(object obj)
        {
            if (obj is Node other) return Label.CompareTo(other.Label);
            throw new Exception("Incompatible comparison");
        }

        public static bool operator ==(Node n1, Node n2)
        {
            if (ReferenceEquals(n1, n2)) return true;

            if (n1 is null && n2 is null) return true;
            if (n1 is null || n2 is null) return false;

            return n1.Label == n2.Label;
        }

        public static bool operator !=(Node n1, Node n2)
        {
            return !(n1 == n2);
        }
    }
}
