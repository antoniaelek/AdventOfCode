using System.Linq;

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

        public int Value()
        {            
            if (ChildrenCount == 0)
                return Metadata.Sum();

            int value = 0;

            foreach(var idx in Metadata)
            {
                if (idx == 0 || idx > ChildrenCount) continue;
                value += Children[idx - 1].Value();
            }

            return value;
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
