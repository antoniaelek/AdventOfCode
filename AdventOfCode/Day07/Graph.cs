using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day07
{
    public class Graph
    {
        public HashSet<Node[]> Edges { get; set; } = new HashSet<Node[]>();
        public HashSet<Node> Vertices { get; set; } = new HashSet<Node>();
        
        public Graph() { }

        public Graph(string inputFile)
        {
            var lines = File.ReadAllLines(inputFile);
            foreach (var line in lines)
            {
                if (!ParseLine(line, out char n1, out char n2))
                {
                    // TODO log
                    continue;
                }

                Vertices.Add(new Node(n1));
                Vertices.Add(new Node(n2));
                Edges.Add(new Node[2] { new Node(n1), new Node(n2) });
            }
        }

        public Graph(HashSet<Node[]> edges, HashSet<Node> vertices)
        {
            Edges = edges ?? throw new ArgumentNullException(nameof(edges));
            Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
        }

        public static Graph Clone(Graph other)
        {
            var copy = new Graph();
            foreach (var v in other.Vertices)
            {
                copy.Vertices.Add(new Node(v.Label));
            }

            foreach (var e in other.Edges)
            {
                copy.Edges.Add(new Node[] { e[0], e[1] });
            }

            return copy;
        }

        public IEnumerable<Node> TopologicalSort()
        {
            var sort = new List<Node>();
            var g = new Graph(Edges, Vertices);
            while (g.Vertices.Any())
            {
                var vertices = g.Vertices.Where(v => InDegree(v) == 0).OrderBy(v => v);
                var selected = vertices.FirstOrDefault();
                sort.Add(selected);
                g.Vertices.Remove(selected);
                g.Edges.RemoveWhere(e => e[0] == selected);
            }
            return sort;
        }

        public int InDegree(Node vertex)
        {
            return Edges.Count(e => e[1] == vertex);
        }

        private static bool ParseLine(string line, out char n1, out char n2)
        {
            n1 = n2 = '\\';
            var split = line.Split(' ');
            if (split.Length < 10) return false;
            n1 = split[1].FirstOrDefault();
            n2 = split[7].FirstOrDefault();
            return true;
        }
    }
}
