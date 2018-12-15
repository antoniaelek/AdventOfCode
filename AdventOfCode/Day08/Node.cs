using System.Collections.Generic;

namespace AdventOfCode.Day08
{
    public class Node
    {
        public Node[] Children { get; }
        public int[] Metadata { get; }
        public int ChildrenCount { get; }
        public int MetadataCount { get; }

        public Node(int childrenCount, int metadataCount)
        {
            ChildrenCount = childrenCount;
            Children = new Node[childrenCount];
            MetadataCount = metadataCount;
            Metadata = new int[metadataCount];
        }

        public static (Node node, int index) Construct(int idx, int[] input)
        {
            var node = new Node(input[idx], input[++idx]);

            if (node.ChildrenCount != 0)
            {
                ++idx;
                for (var i = 0; i < node.ChildrenCount; i++)
                {
                    var curr = Construct(idx, input);
                    node.Children[i] = curr.node;
                    idx = curr.index;
                }
            }
            else
            {
                ++idx;
            }

            for (var i = 0; i < node.MetadataCount; i++)
            {
                node.Metadata[i] = input[idx];
                idx++;
            }

            return (node, idx);
        }
    }
}
