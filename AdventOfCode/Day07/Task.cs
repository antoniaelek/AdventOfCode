using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day07
{
    public static class Task
    {
        public static void Solve()
        {
            var nodes = Parse();
            Print(nodes);
        }

        public static void Print(IEnumerable<Node> nodes)
        {
            foreach(var node in nodes)
            {
                Console.WriteLine($"[{string.Join(", ", node.Prev)}]-[{node.Label}]-[{string.Join(", ", node.Next)}]");
            }
        }

        public static IEnumerable<Node> Parse(string inputFile = "inputs/day07.txt")
        {
            var result = new HashSet<Node>();
            var lines = File.ReadAllLines(inputFile);
            foreach (var line in lines)
            {
                if (!ParseLine(line, out string n1, out string n2))
                {
                    // TODO log
                    continue;
                }

                UpdateNodesList(ref result, n1, n2);
            }

            return result;
        }

        private static bool ParseLine(string line, out string n1, out string n2)
        {
            n1 = n2 = string.Empty;
            var split = line.Split(' ');
            if (split.Length < 10) return false;
            n1 = split[1];
            n2 = split[7];
            return true;
        }

        private static void UpdateNodesList(ref HashSet<Node> nodes, string label1, string label2)
        {
            var n1 = new Node(label1);
            var n2 = new Node(label2);

            nodes.TryGetValue(n1, out n1);
            nodes.TryGetValue(n2, out n2);

            n1 = n1 ?? new Node(label1);
            n2 = n2 ?? new Node(label2);

            n1.Next.Add(label2);
            n2.Prev.Add(label1);

            nodes.Add(n1);
            nodes.Add(n2);
        }

    }
}
