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
            var first = nodes.Where(n => !n.Prev.Any()).ToList();
            //PrintWithDependencies(nodes);
            var sorted = SortByExecutionOrder(nodes);
            Print(sorted);
        }

        public static void PrintWithDependencies(IEnumerable<Node> nodes)
        {
            foreach(var node in nodes)
            {
                Console.WriteLine($"[{string.Join(", ", node.Prev)}]-[{node.Label}]-[{string.Join(", ", node.Next)}]");
            }
        }

        public static void Print(IEnumerable<Node> nodes, string separator = "")
        {
            Console.WriteLine(string.Join(separator, nodes.Select(n => n.Label)));
        }

        public static IEnumerable<Node> SortByExecutionOrder(IEnumerable<Node> nodes)
        {
            var nodesSet = new HashSet<Node>(nodes);
            var firsts = nodesSet.Where(n => !n.Prev.Any()).OrderBy(n => n.Label);
            var prev = new HashSet<string>();
            foreach(var f in firsts?.Skip(1) ?? new List<Node>())
            {
                prev.Add(f.Label);
            }
            var curr = firsts.FirstOrDefault();
            var next = curr?.Next ?? new HashSet<string>();
            foreach(var p in curr?.Prev ?? new HashSet<string>())
            {
                prev.Add(p);
            }
            var seen = new List<Node>();
            while(next.Any())
            {
                // add curr to SEEN
                seen.Add(curr);

                // choose new current from next or prev first alphabetically 
                // that satisfies condition new.prevs IN seen
                var join = next;
                foreach (var p in prev)
                {
                    join.Add(p);
                }
                var newCurr = PickNewCurrent(nodesSet, join, seen.Select(n=>n.Label).ToList());

                // add all other old current's nexts to prev
                // (unless they are in new curr's nexts) 
                foreach (var n in curr.Next)
                {
                    if (!newCurr.Next.Contains(n)) prev.Add(n);
                }

                // and optionally remove from prev the one that is new current
                prev.Remove(newCurr.Label);

                // set new current
                curr = newCurr;

                // set new next as current.next
                next = curr.Next;
            }
            seen.Add(curr);
            return seen;
        }

        private static Node PickNewCurrent(HashSet<Node> allNodes, 
            HashSet<string> pickFrom, List<string> seenNodes)
        {
            foreach (var l in pickFrom.OrderBy(j => j))
            {
                var newCurr = new Node(l);
                allNodes.TryGetValue(newCurr, out newCurr);
                newCurr = newCurr ?? new Node(l);
                var satisfies = true;
                foreach (var n in newCurr.Prev)
                {
                    if (!seenNodes.Contains(n))
                    {
                        satisfies = false;
                        break;
                    }
                }
                if (satisfies) return newCurr;
            }
            throw new Exception("None node satisfies conditions to be picked as next in execution order");
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
