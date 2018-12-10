using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day07
{
    public static class Task
    {
        public static int BaseDuration = 60;
        public static int NoWorkers = 5;

        public static void Solve()
        {
            var graph = new Graph("inputs/day07.txt");

            // Pt 1
            var sorted = Graph.Clone(graph).TopologicalSort();
            Console.WriteLine(string.Join("", sorted.Select(e => e.Label)));

            var sortedExt = sorted.Select(n => new NodeExt(n.Label, BaseDuration));
            var todo = new List<NodeExt>(sortedExt);
            var done = new HashSet<NodeExt>();
            var doing = new HashSet<NodeExt>();
            var workers = new NodeExt[NoWorkers];

            // Pt 2
            Console.WriteLine();
            PrintHeader(workers);

            var timer = 0;
            while (todo.Any() || doing.Any())
            {
                for (int i = 0; i < workers.Length; i++)
                {
                    // If worker has no task assigned, assign new
                    if (workers[i] is null && todo.Any())
                    {
                        AssignTaskToWorker(todo, done, doing, workers, timer, i, graph);
                        continue;
                    }

                    // If worker has task assigned, check if it has finished and assign new
                    if (workers[i] != null && IsFinished(workers[i], timer))
                    {
                        RemoveTaskFromWorker(done, doing, workers, timer, i, ref graph);
                        AssignTaskToWorker(todo, done, doing, workers, timer, i, graph);
                    }
                }
                Print(workers, timer, done);
                timer++;
            };
        }

        private static void RemoveTaskFromWorker(HashSet<NodeExt> done, HashSet<NodeExt> doing,
            NodeExt[] workers, int timer, int i, ref Graph graph)
        {
            workers[i].End(timer);
            done.Add(workers[i]);
            doing.Remove(workers[i]);
            graph.Vertices.Remove(workers[i]);
            graph.Edges.RemoveWhere(e => e[0] == workers[i]);
            workers[i] = null;
        }

        private static void AssignTaskToWorker(List<NodeExt> todo, HashSet<NodeExt> done,
            HashSet<NodeExt> doing, NodeExt[] workers, int timer, int i, Graph g)
        {
            var next = todo.Where(n => g.InDegree(n) == 0).FirstOrDefault();

            // If there is task to be assigned
            if (next != null)
            {
                workers[i] = next;
                workers[i].Start(timer);
                doing.Add(next);
                todo.RemoveAt(todo.IndexOf(next));
            }
        }

        private static void PrintHeader(NodeExt[] workers)
        {
            const int width = -5;
            Console.Write($"{"sec",width}");
            for (int i = 1; i <= workers.Length; i++)
            {
                var name = $"w{i}";
                Console.Write($"{name,width}");
            }
            Console.WriteLine($"{"done",width}");
        }

        private static void Print(NodeExt[] workers, int sec, HashSet<NodeExt> done)
        {
            const int width = -5;
            Console.Write($"{sec,width}");
            for (int i = 1; i <= workers.Length; i++)
            {
                Console.Write($"{workers[i - 1]?.Label ?? '.',width}");
            }
            Console.WriteLine(string.Join(", ", done?.Select(l => l.Label)));
        }

        private static bool IsFinished(NodeExt node, int timer)
        {
            if (node.Running)
            {
                if (node.StartedAt + node.Duration == timer)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
