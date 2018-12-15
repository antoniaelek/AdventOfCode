using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day08
{
    public class Tree
    {
        public int[] Input { get; }
        public Node Root { get; }

        public Tree(string inputFile = "inputs/day08.txt")
        {
            Input = File.ReadAllText(inputFile)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => int.Parse(i))
                .ToArray();
            Root = Node.Construct(0, Input).node;
        }

        public bool Test()
        {
            var traversal = Traverse().ToArray();
            if (traversal.Count() != Input.Count()) return false;
            for (var i = 0; i < traversal.Count(); i++)
            {
                if (traversal[i] != Input[i]) return false;
            }
            return true;
        }

        public IEnumerable<int> Traverse()
        {
            var ret = new List<int>();

            Traverse(Root, ref ret);

            return ret;
        }

        private void Traverse(Node node, ref List<int> traversal)
        {
            traversal.Add(node.ChildrenCount);
            traversal.Add(node.MetadataCount);
            foreach (var child in node.Children)
            {
                Traverse(child, ref traversal);
            }

            foreach (var metadata in node.Metadata)
            {
                traversal.Add(metadata);
            }
        }

        public int SumMetadata()
        {
            return SumMetadata(Root, 0);
        }

        private int SumMetadata(Node node, int sum)
        {
            foreach (var child in node.Children)
            {
                sum = SumMetadata(child, sum);
            }
            sum += node.Metadata.Sum();
            return sum;
        }
    }
}
